using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MovingTarget
{
    public int numberOfPositions = 3;

    protected List<Vector3> targetpositions = new List<Vector3>();
    protected int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfPositions; i++)
        {
            targetpositions.Add(PositionManager.instance.GetRandomPosition());
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(step < targetpositions.Count)
        {
            transform.position = Vector3.Lerp(transform.position, targetpositions[step], speed);
            if (Vector3.Distance(transform.position, targetpositions[step]) < 0.1f)
            {
                step++;
            }

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetpositions[targetpositions.Count - 1] + (Vector3.down * 10), speed);
            if(Vector3.Distance(transform.position, targetpositions[targetpositions.Count - 1] + (Vector3.down * 10)) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
