using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isNight = false;
    [SerializeField] int vigilantes = 4; 
    [SerializeField] EnemyStats enemyStats = null;
    [SerializeField] Transform startArea = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject playerCamera = null;
    [SerializeField] VigilantPlacer vigilantPlacer = null;
    [SerializeField] Light sun = null;
    [SerializeField] float sunNightValue = 100;
    [SerializeField] float sunDayValue = 1000;
    internal static bool allSet;
    internal static bool onekill;
    internal static Vector3 playerPosition;

    private void Start() {
        ActivateNight(false);
        allSet = false;
        vigilantPlacer.SetUp(vigilantes);
    }
    private void Update() {
        if(Input.GetButtonDown("Jump")){
            if(allSet && isNight == false){
                isNight = true;
                StartCoroutine(enemyStats.UpdateMovement());
                ActivateNight(isNight);
            }
        }
        playerPosition = player.transform.position;
    }
    
    public void GoalReached()
    {
        vigilantPlacer.SetUp(vigilantes);
        allSet = false;
        isNight = false;
        ActivateNight(isNight);
    }
    private void ActivateNight(bool night)
    {
        player.transform.position = startArea.position;
        player.SetActive(night);
        playerCamera.SetActive(night);
        sun.intensity = night? sunNightValue : sunDayValue;
    }
}
