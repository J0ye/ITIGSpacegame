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
            if(!isDamaged) GameManager.instance.DamageShip();
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
        spriteRenderer.DOColor(collisionColor, collisionAnimationDuration);
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
        }
        yield return new WaitForSeconds(collisionAnimationDuration);
        spriteRenderer.color = startColor;
        isDamaged = false;
    }
    
    public virtual IEnumerator Damage(float damageValue)
    {
        if (isDamaged)
        {
            yield break;
        }
        isDamaged = true;
        spriteRenderer.DOColor(collisionColor, collisionAnimationDuration);
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            yield return new WaitForSeconds(collisionAnimationDuration * Time.deltaTime);
            spriteRenderer.color = startColor;
            isDamaged = false;
        }
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
