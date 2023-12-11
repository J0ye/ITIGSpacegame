using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DispenserTypes {  Fuel}

public class Dispenser : Interactable
{
    public DispenserTypes dType = DispenserTypes.Fuel;
    public LayerMask interactionMask = new LayerMask();
    public float radius = 10;

    private bool isCollected = false;

    public override void Interact()
    {
        // OG Spherecast is actually named overlapSphere
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        print("Interacting with dispenser");

        foreach(Collider c in hitColliders)
        {
            if (!isCollected && c.gameObject.CompareTag("Player"))
            {
                Debug.Log("Pick up item from dispenser");
                isCollected = true;
                Invoke(nameof(SetCollectedToFlase), 2f);
            }
        }
    }

    private void SetCollectedToFlase()
    {
        isCollected = false;
    }

    void OnDrawGizmos()
    {
        // Set the color for drawing
        Gizmos.color = Color.red;

        // Draw the sphere at the start of the ray
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
