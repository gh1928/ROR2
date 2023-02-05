using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recoil : MonoBehaviour
{    
    public float recoilLimit = 1f;
    public float timeScaler = 2f;

    private float recoil = 0f;
    public float crossHairNormalSize = 50f;
    public float crossHairRecoilSize = 60f;
    public RectTransform crossHair;
    
    void Update()
    {
        CurrRecoil();
        UpdateCrossHair();
    }
    public Vector3 CurrRecoil()
    {        

        if (Input.GetMouseButton(0))
        {
            recoil += Time.deltaTime / timeScaler;
            if(recoil > recoilLimit)
            {
                recoil = recoilLimit;
            }
        }
        else
        {
            recoil -= Time.deltaTime  * 2f / timeScaler;
            if(recoil < 0f)
            {
                recoil = 0f;
            }
        }

        return Random.onUnitSphere * recoil * 0.01f;
    }
    private void UpdateCrossHair()
    {
        float size = Mathf.Lerp(crossHairNormalSize, crossHairRecoilSize, recoil / recoilLimit);
        crossHair.sizeDelta = Vector2.one * size;
    }
}
