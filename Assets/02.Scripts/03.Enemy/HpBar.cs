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
    private EnemyBase enemyBase;
    
    private bool isInstantiated = false;

    public float barWidth = 100f;
    public float barHeigth = 5f;
        
    float timer = 0f;
    private float prevValue;
    public float maxTime = 3f;

    bool isOn = false;

    private int currLevel;

    // Start is called before the first frame update
    void Start()
    {
        canvasHpBar = HpBarUI.Instance.gameObject;
        stats = GetComponent<Stats>();
        enemyBase = GetComponent<EnemyBase>();
        if(!isInstantiated)
        {
            hpBar = Instantiate(hpBar, canvasHpBar.transform);
            isInstantiated = true;
            hpBar.gameObject.SetActive(true);
            hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(barWidth, barHeigth);
        }
        canvasGroup = hpBar.GetComponent<CanvasGroup>();        
        prevValue = stats.Health;
    }    
    void Update()
    {
        UpdateBar();
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
    private void UpdateBar()
    {
        pos = transform.position;
        pos.y += yPosPlus;
        hpBar.transform.position = Camera.main.WorldToScreenPoint(pos);

        canvasGroup.alpha = isOn ? 1f : 0f;

        if (hpBar.transform.position.z < 0f)
            canvasGroup.alpha = 0f;

        hpBar.value = stats.Health / enemyBase.MaxHp;
    }
    public void DieTriggered()
    {
        Invoke(nameof(BarStop), 1f);        
    }
    private void BarStop()
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
