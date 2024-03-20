using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField] Transform transform;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x, y, z, Space.World);
    }
}
