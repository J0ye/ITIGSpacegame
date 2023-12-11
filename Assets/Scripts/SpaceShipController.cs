using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShipController : MonoBehaviour
{
    public GameManager gm;
    public float speed = 100f;
    public float projectileVelocity = 10f;
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(projectile, transform.position + transform.up, projectile.transform.rotation);
            Vector3 v = transform.up * projectileVelocity;
            obj.GetComponent<Rigidbody2D>().velocity = v;
            Destroy(obj, 4f);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gm.EndCycle();
    }
}
