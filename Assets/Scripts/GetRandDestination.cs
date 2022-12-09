using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandDestination : MonoBehaviour
{
    private float x_size, z_size;
    public Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        x_size = gameObject.GetComponent<Renderer>().bounds.size.x;
        z_size = gameObject.GetComponent<Renderer>().bounds.size.z;
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 randDest()
    {
        dest = gameObject.transform.position + new Vector3(Random.Range(-x_size / 2, x_size / 2), 0.0f, Random.Range(-z_size / 2, z_size / 2));
        return dest;
    }
}
