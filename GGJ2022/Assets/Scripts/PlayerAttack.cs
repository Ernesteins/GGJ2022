using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPoint = null;
    [SerializeField, Range(0.01f,3)] float attackRadius = 0.3f;
    [SerializeField] LayerMask enemyLayer = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Attack();
        }
    }

  private void Attack()
  {
    Collider[] hitEnemys = Physics.OverlapSphere(attackPoint.position,attackRadius,enemyLayer);
    foreach (var other in hitEnemys)
    {
        Debug.Log(other.name);
        if(other.TryGetComponent<Enemy>(out Enemy enemy)){
            enemy.Die();
            return;
        }       
    }
  }
  private void OnDrawGizmosSelected() {
      Gizmos.color = Color.yellow;
      Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
  }
}
