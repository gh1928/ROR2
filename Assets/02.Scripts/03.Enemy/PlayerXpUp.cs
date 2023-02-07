using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXpUp : MonoBehaviour
{
    public float exp = 6f;
    public void DieTriggered()
    {
        PlayerLevel.Instance.CurrXp += exp;
    }
}
