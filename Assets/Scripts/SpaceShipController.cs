using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShipController : MonoBehaviour
{
    public float speed = 100f;    

    protected Rigidbody2D rb;
    protected CharacterController cc;
    protected Vector2 inputVector = new Vector2();

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

    private void FixedUpdate()
    {
        rb.velocity = inputVector * speed;
    }

    private void OnDestroy()
    {
        GameManager.instance.EndCycle();
    }
}
