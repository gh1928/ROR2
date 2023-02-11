using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recoil : MonoBehaviour
{    
    public PlayerBase player;

    public float recoilLimit = 1f;
    public float timeScaler = 2f;
    public float recoilScaler = 0.1f;

    private float recoil = 0f;
    public float crossHairNormalSize = 50f;
    public float crossHairRecoilSize = 60f;
    public RectTransform crossHair;

    void Update()
    {
        PlusRecoil();
        MinusRecoil();
        CurrRecoil();
        UpdateCrossHair();
    }
    private void PlusRecoil()
    {
        if (player.IsSprint)
            return;

        if (Input.GetMouseButton(0))
        {
            recoil += Time.deltaTime / timeScaler;
            if (recoil > recoilLimit)
            {
                recoil = recoilLimit;
            }
        }
    }
    private void MinusRecoil()
    {
        if (!Input.GetMouseButton(0) || player.IsSprint)
        {
            recoil -= Time.deltaTime * 2f / timeScaler;
            if (recoil < 0f)
            {
                recoil = 0f;
            }
        }
    }
    public Vector3 CurrRecoil()
    {        
        return recoil * recoilScaler * Random.onUnitSphere; 
    }
    private void UpdateCrossHair()
    {
        float size = Mathf.Lerp(crossHairNormalSize, crossHairRecoilSize, recoil / recoilLimit);
        crossHair.sizeDelta = Vector2.one * size;
    }
}
