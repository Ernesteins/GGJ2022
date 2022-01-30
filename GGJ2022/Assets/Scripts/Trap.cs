using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            other.GetComponent<PlayerAttack>().Discovered();
            CanvasController.DisplayMessage("Don't touch the traps...");
        }
    }
}
