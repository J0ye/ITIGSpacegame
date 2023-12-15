using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    public override void OnActivate()
    {
        GameObject obj = Instantiate(projectile, transform.position + transform.up, projectile.transform.rotation);
        Vector3 v = transform.up * projectileVelocity;
        obj.GetComponent<Rigidbody2D>().velocity = v;
        Destroy(obj, 4f);
    }

    public override void OnEquip()
    {
        projectile = Resources.Load<GameObject>("Capsule");
    }
}
