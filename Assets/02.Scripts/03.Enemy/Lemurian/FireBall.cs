using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireBall.asset", menuName = "Attack/FireBall")]
public class FireBall : AttackDef
{    
    public GameObject prefab;
    public Transform muzzle { set; get; }

    private bool isInstanced;
    private GameObject bullet;
    Vector3 instantPos;
    private void Awake()
    {
        Debug.Log(1);
        instantPos = muzzle.position;
        bullet = Instantiate(prefab, instantPos, Quaternion.identity);
        bullet.SetActive(false);
    }
    public override void ExecuteAttack(GameObject attacker, GameObject defender, Vector3 pos)
    {
        if (defender == null)
            return;

        bullet.SetActive(true);
        bullet.transform.position = instantPos;

        var defenderPos = defender.transform.position;
        defenderPos.y += 1f;
        bullet.transform.LookAt(defenderPos);

        var aStats = attacker.GetComponent<Stats>();
        var dStats = defender.GetComponent<Stats>();
        var attack = CreatAttack(aStats, dStats);

        bullet.GetComponent<Projectile>().Attack = attack;
    } 
}
