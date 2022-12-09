using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text HP;
    public TMP_Text Coin;

    public TMP_Text Wood;
    public TMP_Text Stone;
    public TMP_Text Food;

    public TMP_Text Phase;
    public TMP_Text ATKLv;
    public GameObject GameOverUI;
    public TMP_Text GameTime;

    public TMP_Text ResourceShortage;
    public GameObject ResourceShortageUI;

    private float showTime = 2;
    private float sTime;

    private bool resourceShortage = false;


    [AddComponentMenu("UI.DebugTextComponentName", 11)]
    // Start is called before the first frame update
    void Start()
    {
        GameOverUI.gameObject.SetActive(false);
        ResourceShortageUI.gameObject.SetActive(false);
        sTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (resourceShortage)
        {
            sTime += Time.deltaTime;
            if (sTime >= showTime)
            {
                sTime = 0;
                resourceShortage = false;
                ResourceShortageUI.SetActive(false);
            }
        }

    }

    public void TextChange(Enumeration.ResourceType resource, int text)
    {
        if (resource == Enumeration.ResourceType.HP)
        {
            HP.text = text.ToString();
        }
        else if (resource == Enumeration.ResourceType.Coin)
        {
            Coin.text = text.ToString();
        }
        else if (resource == Enumeration.ResourceType.Food)
        {
            Food.text = text.ToString();
        }
        else if (resource == Enumeration.ResourceType.Stone)
        {
            Stone.text = text.ToString();
        }
        else if (resource == Enumeration.ResourceType.Wood)
        {
            Wood.text = text.ToString();
        }
        else
        {
            GameTime.text = "Defense Time: " + text.ToString() + "s";
        }
    }

    public void PhaseChange(int phase)
    {
        Phase.text = phase.ToString() + " Phase";
    }

    public void GameOver()
    {
        GameOverUI.gameObject.SetActive(true);
    }

    public void ShowResourceShortage(string str)
    {
        ResourceShortage.text = str;
        resourceShortage = true;
        ResourceShortageUI.SetActive(true);
    }

    public void ATkLvChange(int lv)
    {
        ATKLv.text = "ATK Lv." + lv.ToString();
    }
}