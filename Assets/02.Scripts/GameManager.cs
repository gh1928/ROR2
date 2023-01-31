using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float globalGravity = 9.8f;    
    //private bool isPlayScene = true;
    private void Start()
    {
        Cursor.visible = false;
    }

}
