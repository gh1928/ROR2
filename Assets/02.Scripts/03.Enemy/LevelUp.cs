using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private int currLevel;
    private Stats stats;
    private void Start()
    {
        currLevel = GameInfo.Instance.GameLevel;
        stats = GetComponent<Stats>();
    }
    private void Update()
    {
        if(CheckLevelUp())
        {
            stats.Health += stats.HealthInc;
        }        
    }
    private bool CheckLevelUp()
    {
        var level = GameInfo.Instance.GameLevel;
        if (currLevel == level)
            return false;

        currLevel = level;
        return true;
    }
}
