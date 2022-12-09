using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Initial: gold(150), wood(0), stone(0), food(0);
    public int gold, wood, stone, food;
    private UIManager manager;

    private void Start()
    {
        manager = GameObject.Find("UIManager").GetComponent<UIManager>();
        manager.TextChange(Enumeration.ResourceType.Coin, gold);
        manager.TextChange(Enumeration.ResourceType.Wood, wood);
        manager.TextChange(Enumeration.ResourceType.Stone, stone);
        manager.TextChange(Enumeration.ResourceType.Food, food);
    }

    public void addResource(Enumeration.ResourceType resource)
    {
        if (resource == Enumeration.ResourceType.Wood)
        {
            wood += 1;
            manager.TextChange(Enumeration.ResourceType.Wood, wood);
        }
        else if (resource == Enumeration.ResourceType.Stone)
        {
            stone += 1;
            manager.TextChange(Enumeration.ResourceType.Stone, stone);
        }
        else
        {
            food += 1;
            manager.TextChange(Enumeration.ResourceType.Food, food);
        }
    }

    public bool buyResource()
    {
        if (gold < 20)
            return false;
        gold -= 20;
        manager.TextChange(Enumeration.ResourceType.Coin, gold);
        return true;
    }

    public bool sellResource(Enumeration.ResourceType resource)
    {
        if (resource == Enumeration.ResourceType.Wood)
        {
            if (wood > 0)
            {
                wood -= 1;
                gold += 10;
                manager.TextChange(Enumeration.ResourceType.Wood, wood);
                manager.TextChange(Enumeration.ResourceType.Coin, gold);
                return true;
            }
            return false;
        }
        else if (resource == Enumeration.ResourceType.Stone)
        {
            if (stone > 0)
            {
                stone -= 1;
                gold += 10;
                manager.TextChange(Enumeration.ResourceType.Stone, stone);
                manager.TextChange(Enumeration.ResourceType.Coin, gold);
                return true;
            }
            return false;
        }
        else
        {
            if (food > 0)
            {
                food -= 1;
                gold += 10;
                manager.TextChange(Enumeration.ResourceType.Food, food);
                manager.TextChange(Enumeration.ResourceType.Coin, gold);
                return true;
            }
            return false;
        }
    }

    public bool upgrade(Enumeration.ResourceType resource)
    {
        if (resource == Enumeration.ResourceType.Wood)
        {
            if (wood >= 5 && gold >= 20)
            {
                wood -= 5;
                gold -= 20;
                manager.TextChange(Enumeration.ResourceType.Wood, wood);
                manager.TextChange(Enumeration.ResourceType.Coin, gold);
                return true;
            }
            return false;
        }
        else
        {
            if (stone >= 5 && gold >= 20)
            {
                stone -= 5;
                gold -= 20;
                manager.TextChange(Enumeration.ResourceType.Stone, stone);
                manager.TextChange(Enumeration.ResourceType.Coin, gold);
                return true;
            }
            return false;
        }
    }

    public bool feed()
    {
        if (food > 0)
        {
            food--;
            manager.TextChange(Enumeration.ResourceType.Food, food);
            return true;
        }
        return false;
    }
}