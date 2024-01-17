using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    public float timeBetweenShots = 0.2f;

    protected float timer = 0f;
    protected bool ready = true;

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Space) && ready)
        {
            OnActivate();
            ready = false;
        }

        if (!ready && timer < timeBetweenShots)
        {
            timer += Time.deltaTime;
        }
        else
        {
            ready = true;
            timer = 0;
        }
        projectileVelocity = GameManager.instance.gunStat.projectileVelocity;
        timeBetweenShots = GameManager.instance.gunStat.fireRate;

    }

    public override void OnActivate()
    {
        GameObject obj = Instantiate(projectile, transform.position + transform.up, projectile.transform.rotation);
        Vector2 inputVector = GameManager.instance.spaceShip.GetComponent<SpaceShipController>().GetInputVector();
        Vector2 projectileDirection = transform.up + new Vector3(0, Mathf.Clamp01(inputVector.y), 0);
        Vector3 v = projectileDirection * projectileVelocity ;
        obj.GetComponent<Rigidbody2D>().velocity = v;
        Collider2D coll = obj.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>());
        Destroy(obj, 4f);
    }

    public override void OnEquip()
    {
        projectile = Resources.Load<GameObject>("Capsule");
        
    }
}
