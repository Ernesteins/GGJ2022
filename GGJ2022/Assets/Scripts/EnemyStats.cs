using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "GGJ2022/EnemyStats")]
public class EnemyStats : ScriptableObject {
    public float speed = 3;
    public float updateTime = 0.5f;
    public bool enemysActive = false;
    private List<Enemy> enemys = new List<Enemy>();
    
    public IEnumerator UpdateMovement(){
        enemysActive = true;
        while (enemysActive)
        {
            foreach (Enemy enemy in enemys)
            {
                enemy.UpdateMovement();
            }
            Debug.Log("UpdateEnemy");
            yield return new WaitForSeconds(updateTime);
        }
    }
    void UpdateStats(){
        foreach (Enemy enemy in enemys)
        {
            enemy.UpdateStats();
        }
    }
    public void AddEnemy(Enemy enemy){
        enemys.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy){
        enemys.Remove(enemy);
    }
}