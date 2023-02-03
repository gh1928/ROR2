using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpBar;
    private CanvasGroup canvasGroup;
    private GameObject canvasHpBar;
    public float yPosPlus = 1f;
    Vector3 pos;
    private Stats stats;
    private float maxHp;    
    private bool isInstantiated = false;

    public float barWidth = 100f;
    public float barHeigth = 5f;
        

    float timer = 0f;
    private float prevValue;
    public float maxTime = 3f;

    bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasHpBar = HpBarUI.Instance.gameObject;
        stats = GetComponent<Stats>();
        
        if(!isInstantiated)
        {
            hpBar = Instantiate(hpBar, canvasHpBar.transform);
            isInstantiated = true;
            hpBar.gameObject.SetActive(true);
            hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(barWidth, barHeigth);
        }
        canvasGroup = hpBar.GetComponent<CanvasGroup>();        
        maxHp = stats.Health;
        prevValue = stats.Health;
    }    

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        pos.y += yPosPlus;
        hpBar.transform.position = Camera.main.WorldToScreenPoint(pos);

        canvasGroup.alpha = isOn ? 1f : 0f;

        if (hpBar.transform.position.z < 0f)
            canvasGroup.alpha = 0f;

        hpBar.value = stats.Health / maxHp;

        CheckDamaged();
        CheckTimer();
    }
    private void OnEnable()
    {
        if(isInstantiated)
        {
            hpBar.gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
        }
        isOn = false;        
    }
    public void DieTriggered()
    {
        hpBar.gameObject.SetActive(false);
        isOn = false;
    }
    private void CheckDamaged()
    {
        if (stats.Health < prevValue)
        {
            isOn = true;
            timer = 0f;            
        }
        prevValue = stats.Health;        
    }
    private void CheckTimer()
    {
        timer += Time.deltaTime;
        if(timer > maxTime)
        {
            isOn = false;            
        }            
    }
}
