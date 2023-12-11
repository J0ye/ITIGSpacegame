using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public static PositionManager instance;

    public List<Transform> positions = new List<Transform>();

    private List<Transform> positionsCopy = new List<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach(Transform t in positions)
        {
            positionsCopy.Add(t);
        }
    }

    public Vector3 GetRandomPosition()
    {
        if(positions.Count < 1)
        {
            foreach (Transform t in positionsCopy)
            {
                positions.Add(t);
            }
        }

        int rand = Random.Range(0, positions.Count);
        Vector3 position = positions[rand].position;
        positions.Remove(positions[rand]);

        return position;
    }
}
