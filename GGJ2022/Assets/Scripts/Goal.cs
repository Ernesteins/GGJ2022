using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameManager gameManager = null;
   private void OnTriggerEnter(Collider other) {
       if(other.tag == "Player"){
           if(GameManager.onekill){
                Debug.Log("You Win!");
                gameManager.GoalReached();
           }
           else{
               Debug.Log("Kill someone before the night ends");
           }
       }
   }
}
