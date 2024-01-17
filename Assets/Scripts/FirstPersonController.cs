using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    private Rigidbody rb;
    private float x = 0.0f;
    private float z = 0.0f;
    public float speed = 500.0f;
    public LayerMask interactionMask;
    private Ray laser;


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
        laser = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(laser, out hit, 10, interactionMask))
        {
            Interactable hitter;
            if(Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.gameObject.TryGetComponent<Interactable>(out hitter))
            {
                hitter.Interact();
            }
            print(hit.collider.gameObject.name);
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 vector3 = transform.right * x + transform.forward * z;
        if(GameManager.instance != null)
        {
            rb.velocity = vector3 * speed * Time.deltaTime * GameManager.instance.gameSpeed;
        } else
        {
            rb.velocity = vector3 * speed * Time.deltaTime;
        }
        //StartCoroutine(nameof(iwas));
        //transform.position = Vector3.Lerp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(laser);
    }
}
