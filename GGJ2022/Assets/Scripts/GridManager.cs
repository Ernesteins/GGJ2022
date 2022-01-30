using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] CustomGrid grid = null;
    [SerializeField] GameObject trapPrefab = null;
    Queue<GameObject> trapsList = new Queue<GameObject>();
    private void Awake() {
        grid.SetUp(transform.position);
    }
    public void SetUp(int trapAmount) {
        DestroyAll();
        StartCoroutine(PlaceTraps(trapAmount));
    }

    private IEnumerator PlaceTraps(int amount)
    {
        Vector2Int randomPos = Vector2Int.zero;
        int maxAttempts = 100;
        yield return null;
        while (amount>0 && maxAttempts > 0)
        {
            randomPos.x = Mathf.RoundToInt(UnityEngine.Random.Range(0,grid.cols));
            randomPos.y = Mathf.RoundToInt(UnityEngine.Random.Range(0,grid.rows));
            if(grid.GetItemAt(randomPos) == GridItemType.empty){
                amount--;
                trapsList.Enqueue(Instantiate(trapPrefab,grid.GetWoldPosition(randomPos),Quaternion.identity,transform));
            }
            maxAttempts--;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        for (int j = 0; j < grid.rows; j++)
        {
            for (int i = 0; i < grid.cols; i++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(i*grid.cellSize,0,j*grid.cellSize),new Vector3(grid.cellSize,0f,grid.cellSize));
            }
        }
    }
    void DestroyAll(){
        foreach (var trap in trapsList)
        {
            Destroy(trap);
            
        }
        trapsList.Clear();
    }
}
