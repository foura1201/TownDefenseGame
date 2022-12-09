using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private EnemyLife EnemyLife;
    private CannonManager CannonManager;
    // Start is called before the first frame update
    void Start()
    {
        EnemyLife = gameObject.transform.parent.gameObject.GetComponent<EnemyLife>();
        CannonManager = GameObject.Find("VariableManagers").transform.GetChild(5).gameObject.GetComponent<CannonManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            EnemyLife.GetHit(CannonManager.CannonStrength);
        }
    }
}