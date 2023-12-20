using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Vector2 randomSpawn = new Vector2(-10, 10);
    public string projectileTag = "Projectile";

    protected Vector3 startPos;
    
    protected virtual void Awake()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(projectileTag))
        {
            Destroy(collision.gameObject);
            SpawnNewTarget();
            Destroy(gameObject);
        }
    }

    protected void SpawnNewTarget()
    {
        Vector3 newPos = new Vector3(Random.Range(randomSpawn.x, randomSpawn.y), startPos.y);
        Instantiate(gameObject, newPos, transform.rotation);
    }
}
