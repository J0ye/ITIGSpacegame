using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class TargetController : MonoBehaviour
{
    public Vector2 randomSpawn = new Vector2(-10, 10);
    public Color collisionColor = Color.red;
    public float collisionAnimationDuration = 1f;
    public string projectileTag = "Projectile";
    public string playerTag = "Projectile";

    protected SpriteRenderer spriteRenderer;
    protected Vector3 startPos;
    protected Color startColor = Color.white;
    protected bool isDamaged = false;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        startPos = transform.position;
        startColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(projectileTag))
        {
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
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
        if (isDamaged)
        {
            yield break;
        }
        isDamaged = true;
        GameManager.instance.IncrementScore(2);
        transform.DOShakeScale(collisionAnimationDuration);
        spriteRenderer.DOColor(collisionColor, collisionAnimationDuration);
        yield return new WaitForSeconds(collisionAnimationDuration);
        spriteRenderer.color = startColor; 
        SpawnNewTarget();
        Destroy(gameObject);
    }

    protected void SpawnNewTarget()
    {
        Vector3 newPos = new Vector3(Random.Range(randomSpawn.x, randomSpawn.y), startPos.y);
        GameObject obj = Instantiate(gameObject, newPos, transform.rotation);
        obj.transform.localScale = Vector3.one;
    }
}
