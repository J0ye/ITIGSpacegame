using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class TargetController : MonoBehaviour
{
    public GameObject collectablePrefab;
    public int maxHealth = 2;
    public int pointsOnKill = 2;    
    public Vector2 randomSpawn = new Vector2(-10, 10);
    public Color collisionColor = Color.red;
    public float collisionAnimationDuration = 1f;
    public string projectileTag = "Projectile";
    public string playerTag = "Projectile";

    protected SpriteRenderer spriteRenderer;
    protected Vector3 startPos;
    protected Color startColor = Color.white;
    protected bool isDamaged = false;

    protected float currentHealth = 0;
    
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        startPos = transform.position;
        currentHealth = maxHealth;
        startColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(projectileTag) && currentHealth > 0)
        {
            Destroy(collision.gameObject);
            StartCoroutine(Damage());
        }
        else if(collision.gameObject.CompareTag(playerTag) && currentHealth > 0)
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
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
        }
        yield return new WaitForSeconds(collisionAnimationDuration);
        spriteRenderer.color = startColor; 
        SpawnNewTarget();
        Destroy(gameObject);
    }
    
    public virtual IEnumerator Damage(float damageValue)
    {
        transform.DOShakeScale(collisionAnimationDuration*Time.deltaTime);
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            Die();
        }
        yield return new WaitForSeconds(collisionAnimationDuration * Time.deltaTime);
    }

    protected void SpawnNewTarget()
    {
        Vector3 newPos = new Vector3(Random.Range(randomSpawn.x, randomSpawn.y), startPos.y);
        GameObject obj = Instantiate(gameObject, newPos, transform.rotation);
        obj.transform.localScale = Vector3.one;
    }

    protected void SpawnCollectable()
    {
        GameObject co = Instantiate(collectablePrefab, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        SpawnCollectable();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.instance.AddToScore(pointsOnKill);
    }
}
