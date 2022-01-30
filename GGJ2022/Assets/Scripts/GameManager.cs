using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isNight = false;
    [SerializeField] int vigilantes = 4; 
    [SerializeField] int traps = 6; 
    [SerializeField] int trapsIncrement = 2; 
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
    public static event Action onGameOver = delegate(){};
    public static event Action onWin = delegate(){};
    bool isGameOver = false;
    bool tutorial = true;
    GridManager gridManager;
    private void Start() {
        gridManager = FindObjectOfType<GridManager>();
        Time.timeScale = 1;
        vigilantPlacer.SetUp(vigilantes);      
        gridManager.SetUp(traps);
        allSet = false;
        onekill = false;
        isNight = false;
        ActivateNight(isNight);
        CanvasController.DisplayMessage("Use the mouse to place villagers to catch the werewolf!");
    }
    private void Update() {
        if(Input.GetButtonDown("Jump")){
            if(allSet && isNight == false){
                isNight = true;
                StartCoroutine(enemyStats.UpdateMovement());
                ActivateNight(isNight);
                if(tutorial){
                    tutorial = false;
                    CanvasController.DisplayMessage("press <i>Space</i> to attack!!!",1.5f);
                }
            }
            if(isGameOver){
                // reloads the scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        playerPosition = player.transform.position;
    }
    public void EnemyDead(){
        onekill = true;
        vigilantes--;
        if(vigilantes < 1){
            Win();
        }
        else{
            CanvasController.DisplayMessage("<b>Run!!</b> Look for the exit before they catch you");
            enemyStats.UpdateEnemies();
        }
    }
    public void GameOver(){
        if(isGameOver == false){
            isGameOver = true;
            CanvasController.GameOver();
            Time.timeScale=0;
            Debug.Log("GameOver...");
        }
    }
    void Win(){
        CanvasController.Win();
        AudioManager.Play(AudioEffect.win);
        isGameOver = true;
        Time.timeScale = 0;
    }
    public void GoalReached()
    {
        if(onekill){
            vigilantPlacer.SetUp(vigilantes);
            allSet = false;
            onekill = false;
            isNight = false;
            ActivateNight(isNight);
            enemyStats.LevelUp();
            traps += trapsIncrement;
            gridManager.SetUp(traps);
        }
    }
    private void ActivateNight(bool night)
    {
        player.transform.position = startArea.position;
        player.SetActive(night);
        playerCamera.SetActive(night);
        sun.intensity = night? sunNightValue : sunDayValue;
        enemyStats.enemysActive = night;
    }
}
