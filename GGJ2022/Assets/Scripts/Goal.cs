using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
   GameManager gameManager = null;
   private void Start() {
       gameManager = FindObjectOfType<GameManager>();
   }
   private void OnTriggerEnter(Collider other) {
       if(other.tag == "Player"){
           if(GameManager.onekill){
                gameManager.GoalReached();
           }
           else{
               CanvasController.DisplayMessage("Kill someone before escaping");
           }
       }
   }
}
