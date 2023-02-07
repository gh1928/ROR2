using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMeshPro;

    public static PlayerLevel Instance { get; private set; }
    public int Level { get; private set; } = 1;    
    public float CurrXp { get; set; } = 0f;
    public float needXp = 70f;
    public float needXpScale = 1.5f;

    private void Awake()
    {
        Instance = this;
    }   
    void Update()
    {
        UpdateNeedXp();
        UpdateSlide();
        UpdateLvText();
    }
    private void UpdateNeedXp()
    {
        if(CurrXp > needXp)
        {
            Level += 1;
            CurrXp -= needXp;
            needXp *= needXpScale;
        }
    }
    private void UpdateSlide()
    {
        slider.value = CurrXp / needXp;
    }
    private void UpdateLvText()
    {
        textMeshPro.text = $"LVL {Level}";
    }
}
