using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableTypes type = CollectableTypes.Gun;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollect(collision.gameObject);
        }
    }

    private void OnCollect(GameObject player)
    {
        Weapon collectedWeapon;
        switch (type)
        {
            case CollectableTypes.Gun:
                // Get Gun
                collectedWeapon = player.AddComponent<GunWeapon>();
                LogCreator.instance.AddLog("collected gun");
                break;
            case CollectableTypes.Laser:
                // Get Laser
                collectedWeapon = player.AddComponent<LaserWeapon>();
                LogCreator.instance.AddLog("collected Laser");
                break;
            default:
                return;
        }
        collectedWeapon.OnEquip();
        Destroy(gameObject);
    }
}

public enum CollectableTypes
{
    Gun,
    Laser
}
