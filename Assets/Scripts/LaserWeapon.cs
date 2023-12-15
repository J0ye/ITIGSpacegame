using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    public LineRenderer lr;

    public override void OnActivate()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (transform.up * projectileVelocity) + transform.position);
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnActivate();
        }
        else
        {
            lr.positionCount = 0;
        }
    }

    public override void OnEquip()
    {
        LineRenderer tmp = Resources.Load<GameObject>("LaserLinePrefab").GetComponent<LineRenderer>();
        lr = gameObject.AddComponent<LineRenderer>();
        // Debug.Log(tmp);
        // Debug.Log(lr);
        lr.material = tmp.sharedMaterial;
        lr.colorGradient = tmp.colorGradient;
        lr.widthCurve = tmp.widthCurve;
    }
}
