using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public GridItemType Type{get => type;}
    [SerializeField] CustomGrid grid = null;
    [SerializeField] GridItemType type = 0;
    Vector3 initialPosition;
    private void Start() {
        grid.AddToGrid(this);
        initialPosition = transform.position;
    }
    public void RemoveFromGrid() {
        grid.RemoveToGrid(initialPosition);
    }
}
