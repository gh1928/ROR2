using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int GameLevel { get; private set; } = 1;
    public float globalGravity = 9.8f;    
    //private bool isPlayScene = true;
    private void Start()
    {
        Cursor.visible = false;
    }

}
