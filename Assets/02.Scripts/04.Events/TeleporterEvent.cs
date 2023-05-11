using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class TeleporterEvent : MonoBehaviour
{
    private bool isBossEventStarted = false;
    private bool isClearEventStarted = false;
    private Transform player;
    private float distanceToPlayer;
    public GameObject bossEventTxt;
    public GameObject clearText;
    public float detectionRange = 4f;
    bool isPlayerInRange;
    public GameObject boss;
    public GameObject forceField;
    public float fieldScale = 500f;

    public Light mainLight;
    private Color lightNormalColor;
    private Color skyboxNormalTint;
    private float lightNormalIntensity;
    private float eventDuration = 3f;
    private float timer = 0f;
    Coroutine coPrepareEvent;
    Coroutine coCameraShake;
    CinemachineImpulseSource impulseSource;
    public float impulseStrenth = 0.1f;
    public Color color = Color.red;

    public Image fadeEffectPanel;
    public float fadeTimer = 3f;
    public GameObject endingTxt;

    private void Awake()
    {
        skyboxNormalTint = RenderSettings.skybox.GetColor("_Tint");        
        player = GameObject.FindWithTag("Player").transform;
        lightNormalColor = mainLight.color;
        lightNormalIntensity = mainLight.intensity;       
        impulseSource = GetComponent<CinemachineImpulseSource>();        
    }
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer < detectionRange;

        bossEventTxt.SetActive(isPlayerInRange && !isBossEventStarted);
        clearText.SetActive(isPlayerInRange && isClearEventStarted);
        TryStartEvent();
        TryClearEvent();

    }
    private void TryStartEvent()
    {
        if(isPlayerInRange &&
            Input.GetKeyDown(KeyCode.E) &&
            !isBossEventStarted)
        {
            coPrepareEvent = StartCoroutine(CoPrepareEvent());
            isBossEventStarted = true;            
        }        
    }

    public void ClearBoss()
    {        
        isClearEventStarted = true;
    }
    private void TryClearEvent()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E) && isClearEventStarted)
        {
            isClearEventStarted = false;
            StartCoroutine(CoFadeOut());
        }
    }
    IEnumerator CoPrepareEvent()
    {
        coCameraShake = StartCoroutine(CoCameraShake());
        while(timer < eventDuration)
        {
            timer += Time.deltaTime;
            mainLight.color = Color.Lerp(lightNormalColor, color, timer / eventDuration);
            var skyBoxTint = Color.Lerp(skyboxNormalTint, color, timer / eventDuration);
            RenderSettings.skybox.SetColor("_Tint", skyBoxTint);
            mainLight.intensity = Mathf.Lerp(lightNormalIntensity, 2, timer / eventDuration);            
            yield return null;
        }
        StopCoroutine(coCameraShake);
        timer = 0f;
        StartEvent();
        var eventHalfTime = eventDuration / 2f;
        while (timer < eventHalfTime)
        {
            timer += Time.deltaTime;
            mainLight.color = Color.Lerp(color, lightNormalColor, timer / eventHalfTime);
            var skyBoxTint = Color.Lerp(color, skyboxNormalTint, timer / eventHalfTime);
            RenderSettings.skybox.SetColor("_Tint", skyBoxTint);
            mainLight.intensity = Mathf.Lerp(2, lightNormalIntensity, timer / eventHalfTime);
            float t = timer / eventHalfTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            forceField.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * fieldScale, t);

            yield return null;
        }   
        RenderSettings.skybox.SetColor("_Tint", skyboxNormalTint);
        mainLight.intensity = lightNormalIntensity;        
        yield break;
    }
    IEnumerator CoCameraShake()
    {
        while(true)
        {
            var randShake = Random.onUnitSphere * impulseStrenth;
            impulseSource.GenerateImpulse(randShake);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator CoFadeOut()
    {
        float time = 0f;
        Color color = Color.black;
        color.a = 0f;

        while(time < fadeTimer)
        {
            time += Time.deltaTime;
            color.a = time / fadeTimer;            
            fadeEffectPanel.color = color;  
            yield return null;
        }

        endingTxt.SetActive(true);
    }
  
    private void StartEvent()
    {
        boss.SetActive(true);
        var pos = transform.position;
        pos.y += 20f;
        boss.transform.position = pos;
    }
}
