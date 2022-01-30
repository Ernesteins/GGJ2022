using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "GGJ2022/EnemyStats")]
public class EnemyStats : ScriptableObject {
    [Header("Normal Stats")]
    [SerializeField] float _speed = 3;
    [SerializeField] float _angularSpeed = 120;
    [SerializeField] float _walkRadius = 10;
    [SerializeField] float _updateTime = 5;
    [SerializeField, Range(0,1)] float _followPlayer = 0;
    [Header("Alert Stats")]
    [SerializeField] float _Speed = 3;
    [SerializeField] float _AngularSpeed = 120;
    [SerializeField] float _WalkRadius = 10;
    [SerializeField] float _UpdateTime = 5;
    [SerializeField, Range(0,1)] float _FollowPlayer = 0;
    [Header("Vision Cone")]
    public float range = 10;
    public float spotAngle = 30;
    public float intensity = 30;
    [Header("Level up multipliers"),Tooltip("When a new day starts, all the stats will be multipied by the values below")]
    [SerializeField, Range(1,2)] float speedMultiplier = 1.1f;
    [SerializeField, Range(1,2)] float angularSpeedMultiplier = 1.1f;
    [SerializeField, Range(1,2)] float walkRadiusMultiplier = 1.1f;
    [SerializeField, Range(0.5f,1.5f)] float updateTimeMultiplier = 0.95f;
    [SerializeField, Range(1f,1.1f)] float followPlayerMultiplier = 1f;
    public float speed{ get => GameManager.onekill? _Speed : _speed; }
    public float angularSpeed{ get => GameManager.onekill? _AngularSpeed : _angularSpeed; }
    public float walkRadius{ get => GameManager.onekill? _WalkRadius : _walkRadius; }
    public float updateTime{ get => GameManager.onekill? _UpdateTime : _updateTime; }
    public float followPlayer{ get => GameManager.onekill? _FollowPlayer : _followPlayer; }
    
    [HideInInspector] public bool enemysActive = false;
    private List<Enemy> enemys = new List<Enemy>();
    
    public void LevelUp(){
        _speed *= speedMultiplier;
        _angularSpeed *= angularSpeedMultiplier;
        _walkRadius *= walkRadiusMultiplier;
        _updateTime *= updateTimeMultiplier;
        _followPlayer *= followPlayerMultiplier;
        _Speed *= speedMultiplier;
        _AngularSpeed *= angularSpeedMultiplier;
        _WalkRadius *= walkRadiusMultiplier;
        _UpdateTime *= updateTimeMultiplier;
        _FollowPlayer *= followPlayerMultiplier;
    }
    public void UpdateEnemies(){
        foreach (Enemy enemy in enemys)
        {
            enemy.UpdateMovement();
        }
    }
    public IEnumerator UpdateMovement(){
        enemysActive = true;
        while (enemysActive)
        {
            UpdateEnemies();
            yield return new WaitForSeconds(updateTime);
        }
    }
    public void AddEnemy(Enemy enemy){
        enemys.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy){
        enemys.Remove(enemy);
    }
}