using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPoint = null;
    [SerializeField, Range(0.01f,3)] float attackRadius = 0.3f;
    [SerializeField] LayerMask enemyLayer = 0;

    GameManager gameManager;
    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump")){
            Attack();
        }
    }

    public void Discovered(){
        gameManager.GameOver();
    }
    private void Attack()
    {
        Collider[] hitEnemys = Physics.OverlapSphere(attackPoint.position,attackRadius,enemyLayer);
        foreach (var other in hitEnemys)
        {
            if(other.TryGetComponent<Enemy>(out Enemy enemy)){
                enemy.Die();
                gameManager.EnemyDead();
                return;
            }       
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
  }
}
