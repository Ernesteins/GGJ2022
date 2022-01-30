using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public GridItemType Type{get => type;}
    [SerializeField] CustomGrid grid = null;
    [SerializeField] GridItemType type = 0;
    private void Start() {
        grid.AddToGrid(this);
    }
    private void OnDistroy() {
        grid.RemoveToGrid(this);
    }
}
