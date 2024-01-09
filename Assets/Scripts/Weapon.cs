using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float projectileVelocity = 10f;
    public GameObject projectile;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnActivate();
        }
        else
        {
            IsInactive();
        }
    }

    public virtual void OnEquip()
    {

    }

    public virtual void OnUnequip()
    {

    }

    public virtual void OnActivate()
    {
        GameObject obj = Instantiate(projectile, transform.position + transform.up, projectile.transform.rotation);
        Vector3 v = transform.up * projectileVelocity;
        obj.GetComponent<Rigidbody2D>().velocity = v;
        Destroy(obj, 4f);
    }

    public virtual void IsInactive()
    {

    }
}
