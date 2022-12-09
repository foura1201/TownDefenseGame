using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public int CannonStrength;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        CannonStrength = 1;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CannonUpgrade()
    {
        CannonStrength += 1;
        uiManager.ATkLvChange(CannonStrength);
    }
}