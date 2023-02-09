using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfo : MonoBehaviour
{
    static public GameInfo Instance { get; private set; }
    public float globalGravity = 9.8f;
    public int GameLevel { get; private set; } = 1;
    public TextMeshProUGUI timerTex;
    public TextMeshProUGUI levelTex;
    public Image bar;

    private float timer = 0f;
    public float needTimeToLevelUp = 140f;
    private bool isStopped = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {        
        UpdateTimer();
        UpdateTimerTex();
        UpdateLevelBar();
        UpdateLevelTex();
        UpdateGameLevel();
    }
    private void UpdateTimerTex()
    {        
        timerTex.text = $"{(int)(timer / 60):00.}:{(int)timer:00.}";
    }
    private void UpdateLevelTex()
    {
        levelTex.text = GameLevel.ToString();
    }
    private void UpdateLevelBar()
    {
        bar.fillAmount = (timer % needTimeToLevelUp) / needTimeToLevelUp;
    }
    private void UpdateTimer()
    {
        if (isStopped)
            return;

        timer += Time.deltaTime;
    }
    private void UpdateGameLevel()
    {
        GameLevel = (int)(timer / needTimeToLevelUp + 1);
    }
}




