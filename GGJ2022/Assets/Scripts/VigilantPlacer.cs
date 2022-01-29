using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigilantPlacer : MonoBehaviour
{
    [SerializeField] CustomGrid grid = null;
    [SerializeField] int vigilantsAmount = 3;
    [SerializeField] GameObject vigilantPrefab = null; 
    [SerializeField] GameObject mousePointer = null;

    [SerializeField] Color validColor = Color.green;
    [SerializeField] Color invalidColor = Color.red;

    Camera cam= null;
    Renderer pointerRenderer;
    private void Start() {
        cam = Camera.main;
        pointerRenderer = mousePointer.GetComponent<Renderer>();
        mousePointer.transform.localScale = new Vector3(grid.cellSize,0.25f,grid.cellSize);
    }
    void Update()
    {
        if(vigilantsAmount<1) return;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)){
            Vector3 pos = grid.SnapToGrid(hit.point);
            RenderMousePointerAt(pos);
            if(Input.GetMouseButtonDown(0)&&grid.isValidPoint(pos)){
                PlaceVigilantNear(hit.point);
                vigilantsAmount--;
                if(vigilantsAmount < 1){
                    DisableMousePointer();
                }
            }        
        }
    }

  private void DisableMousePointer()
  {
    mousePointer.SetActive(false);
  }

  private void RenderMousePointerAt(Vector3 pos)
  {
    if(grid.isValidPoint(pos)){
        pointerRenderer.material.color = validColor;
    }else{
        pointerRenderer.material.color = invalidColor;
    }
    mousePointer.transform.position = pos;
  }

  void PlaceVigilantNear(Vector3 worldPos){
        Vector3 pos = grid.SnapToGrid(worldPos);
        Instantiate(vigilantPrefab,pos,Quaternion.identity);
    }
}
