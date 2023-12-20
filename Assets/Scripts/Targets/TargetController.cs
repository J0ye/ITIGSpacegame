using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class TargetController : MonoBehaviour
{
    public Vector2 randomSpawn = new Vector2(-10, 10);
    public float collisionAnimationDuration = 1f;
    public string projectileTag = "Projectile";
    public string playerTag = "Projectile";

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
        else if(collision.gameObject.CompareTag(playerTag))
        {
            StartCoroutine(HitPlayer(collision.gameObject));
            StartCoroutine(Damage());
        }
    }

    protected virtual IEnumerator HitPlayer(GameObject target)
    {
        target.transform.DOShakeScale(collisionAnimationDuration);
        yield return new WaitForSeconds(collisionAnimationDuration);
        Destroy(target);
    }

    public virtual IEnumerator Damage()
    {
        transform.DOShakeScale(collisionAnimationDuration);
        yield return new WaitForSeconds(collisionAnimationDuration);
        SpawnNewTarget();
        Destroy(gameObject);
    }

    protected void SpawnNewTarget()
    {
        Vector3 newPos = new Vector3(Random.Range(randomSpawn.x, randomSpawn.y), startPos.y);
        Instantiate(gameObject, newPos, transform.rotation);
    }
}
