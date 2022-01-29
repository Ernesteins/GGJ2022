using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isNight = false;
    [SerializeField] Transform startArea = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject playerCamera = null;
    [SerializeField] Light sun = null;
    [SerializeField] float sunNightValue = 100;
    [SerializeField] float sunDayValue = 1000;
    private void Start() {
        ActivateNight(false);
    }
    private void Update() {
        if(Input.GetButtonDown("Jump")){
            isNight = !isNight;
            ActivateNight(isNight);
        }
    }

    private void ActivateNight(bool night)
    {
        player.transform.position = startArea.position;
        player.SetActive(night);
        playerCamera.SetActive(night);
        sun.intensity = night? sunNightValue : sunDayValue;
    }
}
