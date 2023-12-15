using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SearchingEnemy : MovingTarget
{
    public ContactFilter2D filter = new ContactFilter2D();
    public float explosionRadius = 0.5f;
    public float optionalDistance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToPlayer());
    }

    private IEnumerator GoToPlayer()
    {
        yield return new WaitUntil(() => DoesPlayerExist());
        transform.DOMove(GameManager.instance.spaceShip.position, speed);
        yield return new WaitForSeconds(speed);
        if(Vector3.Distance(transform.position, GameManager.instance.spaceShip.position) > optionalDistance)
        {
            StartCoroutine(GoToPlayer());
        }
        else
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            if(Physics2D.CircleCast(transform.position, explosionRadius, Vector3.zero, filter, hits) > 0)
            {
                foreach(RaycastHit2D r in hits)
                {
                    SpaceShipController s;
                    if(r.collider.gameObject.TryGetComponent<SpaceShipController>(out s))
                    {
                        Destroy(r.collider.gameObject);
                        Destroy(gameObject);
                        SpawnNewTarget();
                    }
                }
            }
        }
    }

    private bool DoesPlayerExist()
    {
        if(GameManager.instance.spaceShip != null)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
