using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingTarget : TargetController
{
    public float speed = 10f;

    protected Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = -transform.up * speed;
    }

    protected void Update()
    {
        if (!GameManager.instance.pause)
        {
            if (transform.position.y < -10)
            {
                SpawnNewTarget();

                Destroy(gameObject);
            }
        }
    }
}
