using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShipController : MonoBehaviour
{
    public List<Transform> weaponSlots = new List<Transform>();
    public float speed = 100f;    

    protected Rigidbody2D rb;
    protected CharacterController cc;
    protected Vector2 inputVector = new Vector2();
    protected int slotIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.instance.spaceShip = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(x, y); // (x, y); (0, 0)  
    }

    public Weapon AddWeapon(Type type)
    {
        if(slotIndex < weaponSlots.Count)
        {
            weaponSlots[slotIndex].gameObject.SetActive(true);
            Weapon ret = (Weapon)weaponSlots[slotIndex].gameObject.AddComponent(type);
            slotIndex++;
            return ret;
        }
        else
        {
            GameManager.instance.AddToScore(5);
            return null;
        }
    }

    public bool RemoveWeapon()
    {
        Weapon toBeRemoved;
        GameObject slot = weaponSlots[slotIndex].gameObject;
        if (slotIndex > 0 && slot.TryGetComponent<Weapon>(out toBeRemoved))
        {
            Destroy(toBeRemoved);
            slot.SetActive(false);
            slotIndex--;
            return true;
        }

        return false;
    }

    public void Damage()
    {
        if(!RemoveWeapon())
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = inputVector * speed;
    }

    private void OnDestroy()
    {
        GameManager.instance.EndCycle();
    }
}
