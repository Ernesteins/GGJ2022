using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] Light spotLight = null;
    [SerializeField] LayerMask playerLayer = 0;
    [SerializeField] float rangePersentage = 0.8f;
    [SerializeField] EnemyStats stats = null;
    NavMeshAgent agent = null;
    bool alive = true;
    private void OnEnable() => stats.AddEnemy(this);
    private void OnDisable() => stats.RemoveEnemy(this);
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        spotLight.intensity = 0;
        UpdateStats();
    }
    private void Update() {
        if(alive){
            CheckVisionCone();
        }
    }
    public void UpdateMovement(){
        UpdateStats();
        Vector3 destination = UnityEngine.Random.value > 1 - stats.followPlayer? GameManager.playerPosition : GetRandomDestination();
        agent.SetDestination(destination);
    }

    private Vector3 GetRandomDestination()
    {
        Vector3 randomDir = UnityEngine.Random.insideUnitSphere * stats.walkRadius;
        randomDir += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, stats.walkRadius,1);
        return hit.position;
    }
    void CheckVisionCone(){
        Collider[] player = Physics.OverlapSphere(transform.position,stats.range*rangePersentage,playerLayer);
        if(player.Length > 0){
            Vector3 dir = player[0].transform.position - transform.position;
            float dot = Vector3.Dot(transform.forward,dir.normalized);
            if(dot > 1 - stats.spotAngle/360f){
                Ray ray = new Ray(transform.position,dir);
                if(Physics.Raycast(ray,out RaycastHit hit,stats.range)&&hit.collider == player[0]){
                    Debug.DrawLine(transform.position, hit.point, Color.red, 0.1f);
                    player[0].GetComponent<PlayerAttack>().Discovered();
                }
            }
        }
    }
    public void UpdateStats(){
        agent.speed = stats.speed;
        agent.angularSpeed = stats.angularSpeed;
        spotLight.range = stats.range;
        spotLight.spotAngle = stats.spotAngle;
        spotLight.intensity = stats.intensity;
    }
    public void Die(){
        alive = false;
        stats.RemoveEnemy(this);
        agent.isStopped = true;
        spotLight.intensity = 0;
    }
}
