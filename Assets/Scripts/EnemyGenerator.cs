using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Enemy;
    public float generationTime, x_size, z_size;
    private Vector3 instantiatePos;
    private bool generation = true;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        x_size = gameObject.GetComponent<Renderer>().bounds.size.x;
        z_size = gameObject.GetComponent<Renderer>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > generationTime)
        {
            generation = true;
            time = 0;
        }

        if (generation)
        {
            instantiatePos = new Vector3(Random.Range(-x_size / 2, x_size / 2), 0.0f, Random.Range(-z_size / 2, z_size / 2));
            Instantiate(Enemy, gameObject.transform.position + instantiatePos, Quaternion.identity);
            generation = false;
        }
    }

    public void SetGenTime(float time)
    {
        generationTime = time;
    }
}
