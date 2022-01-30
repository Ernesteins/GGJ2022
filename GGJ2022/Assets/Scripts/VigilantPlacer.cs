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
    Queue<GameObject> vigilantsList = new Queue<GameObject>();
    private void Start() {
        cam = Camera.main;
        pointerRenderer = mousePointer.GetComponent<Renderer>();
        mousePointer.transform.localScale = new Vector3(grid.cellSize,0.25f,grid.cellSize);
    }
    public void SetUp(int amount){
        vigilantsAmount = amount;
        DestroyAll();
        mousePointer.SetActive(true);
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
                AudioManager.Play(AudioEffect.villager);
                vigilantsAmount--;
                if(vigilantsAmount < 1){
                    mousePointer.SetActive(false);
                    GameManager.allSet = true;
                    CanvasController.DisplayMessage("Press <i>space</i> to continue");
                }
            }        
        }
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
            vigilantsList.Enqueue(Instantiate(vigilantPrefab,pos,Quaternion.identity));
    }
    void DestroyAll(){
        foreach (var vigilant in vigilantsList)
        {
            vigilant.GetComponent<GridItem>().RemoveFromGrid();
            Destroy(vigilant);
        }
        vigilantsList.Clear();
    }
}
