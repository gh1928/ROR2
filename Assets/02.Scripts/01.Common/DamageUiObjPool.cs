using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class DamageUiObjPool : MonoBehaviour
{
    public DamageUI normal;
    public DamageUI crit;    
    public IObjectPool<DamageUI> NormalPool { get; private set; }
    public IObjectPool<DamageUI> CritPool { get; private set; }
    private void Awake()
    {
        NormalPool = new ObjectPool<DamageUI>(
            CreateNormal,
            OnGet,
            OnRealese,
            OnDestroyUI,
            maxSize : 128
            );
        CritPool = new ObjectPool<DamageUI>(
            CreateCrit,
            OnGet,
            OnRealese,
            OnDestroyUI,
            maxSize: 128
            );    
    }
    private DamageUI CreateNormal()
    {
        DamageUI normalUi = Instantiate(normal, transform);
        normalUi.SetPool(NormalPool);
        return normalUi;
    }
    private DamageUI CreateCrit()
    {
        DamageUI critUi = Instantiate(crit, transform);
        critUi.SetPool(CritPool);
        return critUi;
    }
    private void OnGet(DamageUI ui)
    {
        ui.gameObject.SetActive(true);
    }
    private void OnRealese(DamageUI ui)
    {
        ui.gameObject.SetActive(false);
    }
    private void OnDestroyUI(DamageUI ui)
    {
        Destroy(ui.gameObject);
    }
}
