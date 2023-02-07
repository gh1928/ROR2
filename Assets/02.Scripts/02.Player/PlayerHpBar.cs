using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHpBar : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    private PlayerLevel level;
    private LevelScaling scailedStats;
    private Stats stats;    
    private void Start()
    {
        level = GetComponent<PlayerLevel>();
        scailedStats = GetComponent<LevelScaling>();
        stats = GetComponent<Stats>();
    }

    private void Update()
    {
        UpdateHpSlider();
        UpdateHpText();
    }

    private void UpdateHpText()
    {
        hpText.text = $"{stats.Health}/{scailedStats.MaxHealth}";
    }

    private void UpdateHpSlider()
    {
        hpSlider.value = stats.Health / scailedStats.MaxHealth;
    }
}
