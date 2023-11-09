using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Vector2 randomSpawn = new Vector2(-10, 10);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 newPos = new Vector3(Random.Range(randomSpawn.x, randomSpawn.y), transform.position.y);
            Instantiate(gameObject, newPos, transform.rotation);
            Destroy(gameObject);
        }
    }
}
