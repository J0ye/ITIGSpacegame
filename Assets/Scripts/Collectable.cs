using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableTypes type = CollectableTypes.Gun;
    public TMP_Text text;
    public int pointValueOnCollect = 1;
    public bool randomizeType = true;
    public float chanceForPoints = 1;

    protected void Awake()
    {
        if(randomizeType)
        {
            RandomizeType();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollect(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollect(GameObject player)
    {
        Weapon collectedWeapon = null;
        switch (type)
        {
            case CollectableTypes.Gun:
                // Get Gun
                collectedWeapon = player.GetComponent<SpaceShipController>().AddWeapon(typeof(GunWeapon));
                LogCreator.instance.AddLog("collected gun");
                break;
            case CollectableTypes.Laser:
                // Get Laser
                collectedWeapon = player.GetComponent<SpaceShipController>().AddWeapon(typeof(LaserWeapon));
                LogCreator.instance.AddLog("collected laser");
                break;
            case CollectableTypes.Points:
                // Get Points
                GameManager.instance.AddToScore(pointValueOnCollect);
                LogCreator.instance.AddLog("collected points +" + pointValueOnCollect.ToString());
                break;
            default:
                return;
        }

        if(collectedWeapon != null)
        {
            collectedWeapon.OnEquip();
        }
    }

    public void RandomizeType()
    {
        int ammountOfTypes = Enum.GetValues(typeof(CollectableTypes)).Length;
        float rand = UnityEngine.Random.Range(0, (ammountOfTypes) * chanceForPoints);
        rand = Mathf.Clamp(rand, 0, ammountOfTypes);
        int randomAsInt = (int)rand;
        type = (CollectableTypes)randomAsInt;
        text.text = type.ToString();
    }
}

public enum CollectableTypes
{
    Gun,
    Laser,
    Points
}
