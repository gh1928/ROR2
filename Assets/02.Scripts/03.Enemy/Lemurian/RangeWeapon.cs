using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon.asset", menuName = "Attack/RangeWeapon")]
public class RangeWeapon : AttackDef
{    
    public GameObject prefab;
    public Transform muzzle { set; get; }

    public bool isInstanced;
    private GameObject bullet;   
  
    public override void ExecuteAttack(GameObject attacker, GameObject defender, Vector3 pos)
    {
        if (defender == null)
            return;

        if (isInstanced == false)
        {            
            bullet = Instantiate(prefab, muzzle.position, Quaternion.identity);
            isInstanced = true;            
        }
        else
        {
            bullet.transform.position = muzzle.position;            
            bullet.SetActive(true);
        }

        var defenderPos = defender.transform.position;
        defenderPos.y += 1f;

        var projectile = bullet.GetComponent<Projectile>();
        projectile.Fire(defenderPos);

        var aStats = attacker.GetComponent<Stats>();
        var dStats = defender.GetComponent<Stats>();
        var attack = CreatAttack(aStats, dStats);

        bullet.GetComponent<Projectile>().Attack = attack;
    } 
}
