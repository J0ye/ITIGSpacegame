using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    private Rigidbody rb;
    private float x = 0.0f;
    private float z = 0.0f;
    public float speed = 500.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Rigidbody>(out rb))
        {
            Debug.LogError("No Rigidbody on player");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        
    }

    private void FixedUpdate()
    {
        Vector3 vector3 = transform.right * x + transform.forward * z;
        rb.velocity = vector3 * speed * Time.deltaTime;
        
    }
}
