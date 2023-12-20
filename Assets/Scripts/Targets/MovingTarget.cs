using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingTarget : TargetController
{
    public float speed = 10f;

    protected Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        rb.velocity = -transform.up * speed * GameManager.instance.gameSpeed * Time.deltaTime;

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
