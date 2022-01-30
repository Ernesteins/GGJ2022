using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyStats stats = null;
    NavMeshAgent agent = null;
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        UpdateStats();
    }
    public void UpdateMovement(){
        agent.SetDestination(GameManager.playerPosition);

    }
    public void UpdateStats(){
        agent.speed = stats.speed;
    }
    public void Die(){
        GameManager.onekill = true;
        Debug.Log("Killed");
    }
}
