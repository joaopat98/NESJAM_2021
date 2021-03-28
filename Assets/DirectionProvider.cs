using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionProvider : MonoBehaviour
{
    public Vector3 position { get => transform.position; }
    public List<Vector3> GetDirections()
    {
        var lst = new List<Vector3>();
        foreach (Transform child in transform)
        {
            lst.Add((child.position - transform.position).normalized);
        }
        return lst;
    }
}
