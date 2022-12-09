using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cannon : MonoBehaviour
{
    public GameObject Bullet;

    private bool Track;
    private float FireTime;
    public float FireInterval;
    // Start is called before the first frame update
    void Start()
    {
        Track = true;
        FireTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (FireTime > FireInterval)
        {
            Debug.Log(FireTime);
            Track = true;
            FireTime = 0;
        }

        if (!Track) FireTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy") && Track)
        {
            Instantiate(Bullet, other.gameObject.transform.transform.position, Quaternion.identity);
            Track = false;
        }
    }
}