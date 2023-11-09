using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShipController : MonoBehaviour
{
    public float speed = 100f;
    public GameObject projectile;

    protected Rigidbody2D rb;
    protected CharacterController cc;
    protected Vector2 inputVector = new Vector2();

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        inputVector = new Vector2(x, y); // (x, y); (0, 0)

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = inputVector * speed;
        /*if(cc != null)
        {
            cc.Move(inputVector * speed);
        }*/
    }
}
