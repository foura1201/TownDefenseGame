using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaborSpawn : MonoBehaviour
{
    public GameObject Hammer;
    public GameObject Axe;
    public GameObject Mine;
    public GameObject Farm;

    public GameObject Glow;
    public GameObject centerPos;
    bool hasSpawn = false;
    float timecount = 0.0f;

    private AudioManager audioManager;

    //public GameObject Enumeration;
    Enumeration enumeration;

    // Start is called before the first frame update
    void Start()
    {
        //enumeration = Enumeration.GetComponent<Enumeration>();
        Glow.SetActive(false);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawn)
        {
            if (timecount < 3)
            {
                timecount += Time.deltaTime;
            }
            else
            {
                hasSpawn = false;
                timecount = 0.0f;
                Glow.SetActive(false);
            }
        }
    }

    public void Spawn(Enumeration.LaborType labor)
    {
        if(labor == Enumeration.LaborType.Hammer)
        {
            Instantiate(Hammer, centerPos.transform.position, Quaternion.identity);
            SetGlowToActive();
        }
        else if (labor == Enumeration.LaborType.Axe)
        {
            Instantiate(Axe, centerPos.transform.position, Quaternion.identity);
            SetGlowToActive();
        }
        else if (labor == Enumeration.LaborType.Farm)
        {
            Instantiate(Farm, centerPos.transform.position, Quaternion.identity);
            SetGlowToActive();
        }
        else
        {
            Instantiate(Mine, centerPos.transform.position, Quaternion.identity);
            SetGlowToActive();
        }

        audioManager.PlayUISound(Enumeration.UISound.LaborSpawn);
    }

    private void SetGlowToActive()
    {
        hasSpawn = true;
        Glow.SetActive(true);
        timecount = 0.0f;
    }
}
