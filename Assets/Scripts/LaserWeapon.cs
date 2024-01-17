using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    public GameObject sparksPrefab;
    public LineRenderer lr;
    public ContactFilter2D filter = new ContactFilter2D();
    public float damagePerSecond = 4.0f;

    protected GameObject sparks;

    private void Awake()
    {
        sparksPrefab = Resources.Load<GameObject>("Sparks");
    }

    public override void OnActivate()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (transform.up * projectileVelocity) + transform.position);
        float laserLength = Vector3.Distance(transform.position, (transform.up * projectileVelocity) + transform.position);

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        if (Physics2D.Raycast(transform.position, transform.up, filter, hits, laserLength) > 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                TargetController tc;
                if(hit.collider.TryGetComponent<TargetController>(out tc))
                {
                    StartCoroutine(tc.Damage(damagePerSecond * Time.deltaTime));
                    SetSparks(hit.point);
                }
                else
                {
                    OnRemoveSparks();
                }
            }
        }
        else
        {
            OnRemoveSparks();
        }
    }

    public override void IsInactive()
    {
        OnRemoveSparks();
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
        projectileVelocity = GameManager.instance.laserStat.projectileVelocity;
        damagePerSecond = GameManager.instance.laserStat.fireRate;
    }

    public override void OnEquip()
    {
        LineRenderer tmp = Resources.Load<GameObject>("LaserLinePrefab").GetComponent<LineRenderer>();
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = tmp.sharedMaterial;
        lr.colorGradient = tmp.colorGradient;
        lr.widthCurve = tmp.widthCurve;
       
    }

    protected void SetSparks(Vector3 targetPosition)
    {
        if(sparks == null)
        {
            sparks = Instantiate(sparksPrefab, targetPosition, sparksPrefab.transform.rotation);
        }
        sparks.transform.position = targetPosition;
    }

    protected void OnRemoveSparks()
    {
        Destroy(sparks);
        sparks = null;
    }
}
