using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartArea : MonoBehaviour
{
    [SerializeField] CustomGrid grid = null;
    void Start()
    {
        Vector3 offset = Vector3.zero;
        for (int y = -2; y < 3; y++)
        {
            offset.z = y*grid.cellSize;
            for (int x = -2; x < 3; x++){
                offset.x = x*grid.cellSize;
                grid.AddToGrid(transform.position + offset, GridItemType.obstacle);   
            }    
        } 
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position,0.3f);
        Gizmos.DrawWireCube(transform.position,new Vector3(grid.cellSize*5,0,grid.cellSize*5));
    }
}
