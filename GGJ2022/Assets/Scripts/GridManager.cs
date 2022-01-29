using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] CustomGrid grid = null;
    private void Awake() {
        grid.SetUp(transform.position);
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        for (int j = 0; j < grid.rows; j++)
        {
            for (int i = 0; i < grid.cols; i++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(i*grid.cellSize,0,j*grid.cellSize),new Vector3(grid.cellSize,0.1f,grid.cellSize));
            }
        }
    }
}
