using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CanvasController : MonoBehaviour
{
    [SerializeField, Range(0.1f,3)] float fadeSpeed = 1;
    [SerializeField] CanvasGroup gameOverPanel = null;
    [SerializeField] CanvasGroup winPanel = null;
    [SerializeField] CanvasGroup messageGroup = null;
    [SerializeField] TextMeshProUGUI messageText = null;

    static CanvasController instance = null;

    void Awake()
    {
        instance = this;
        messageGroup.alpha = 0;
        gameOverPanel.alpha = 0;
        winPanel.alpha = 0;
    }
    IEnumerator FadeInPanel(CanvasGroup panel){
        float t = 0;
        while (t<=1)
        {
            panel.alpha = t;
            t += Time.unscaledDeltaTime * fadeSpeed;
            yield return null;
        }
        panel.alpha = 1;
    }
    IEnumerator DisplayText(string message, float displayTime){
        messageText.text = message;
        float t = 0;
        while (t<=1)
        {
            messageGroup.alpha = t;
            t += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        messageGroup.alpha = 1;
        yield return new WaitForSeconds(displayTime);
        while (t>=0)
        {
            messageGroup.alpha = t;
            t -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
    public static void DisplayMessage(string message, float displayTime = 2){
        instance?.StartCoroutine(instance.DisplayText(message,displayTime));
    }
    public static void GameOver(){
        instance?.StartCoroutine(instance.FadeInPanel(instance.gameOverPanel));
    }
    public static void Win(){
        instance?.StartCoroutine(instance.FadeInPanel(instance.winPanel));
    }
}
