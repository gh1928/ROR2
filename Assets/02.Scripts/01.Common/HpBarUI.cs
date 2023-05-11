using UnityEngine;

public class HpBarUI : MonoBehaviour
{
    static private HpBarUI instance;
    static public HpBarUI Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
    }

}
