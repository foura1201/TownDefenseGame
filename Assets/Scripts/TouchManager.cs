using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private ResourceManager resourceManager;
    private Ray touchRay;
    private RaycastHit hit;

    public GameObject cannonPrefab;
    public static int maxCannonNum = 4;
    private int currindex;
    public GameObject[] cannons = new GameObject[maxCannonNum];
    private Vector3 center;

    public GameObject laborSpawnOBJ;
    private LaborSpawn laborSpawn;
    private CannonManager cannonManager;

    private UIManager UIManager;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = gameObject.transform.parent;

        currindex = 0;
        center = GameObject.Find("Target").transform.position;
        laborSpawn = laborSpawnOBJ.GetComponent<LaborSpawn>();

        resourceManager = parentTransform.GetChild(3).gameObject.GetComponent<ResourceManager>();
        UIManager = parentTransform.GetChild(4).gameObject.GetComponent<UIManager>();
        cannonManager = parentTransform.GetChild(5).gameObject.GetComponent<CannonManager>();
        audioManager = parentTransform.GetChild(6).gameObject.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //raycast manager 통해 터치 인식 후 터치 한 것이 자원이면 자원 object destroy, 자원 수 업데이트

        if (Input.GetMouseButtonDown(0))
        {
            touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(touchRay, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag("R_wood")) //wood, stone, food collect
                {
                    Destroy(obj);
                    resourceManager.addResource(Enumeration.ResourceType.Wood); 
                }
                else if (obj.CompareTag("R_stone")) //wood, stone, food collect
                {
                    Destroy(obj);
                    resourceManager.addResource(Enumeration.ResourceType.Stone);
                }
                else if (obj.CompareTag("R_food")) //wood, stone, food collect
                {
                    Destroy(obj);
                    resourceManager.addResource(Enumeration.ResourceType.Food);
                }
                else if (obj.CompareTag("ToBuy")) //buy한 object에 따라 용병 생성
                {
                    if (resourceManager.buyResource())
                    {
                        //character generate
                        if (obj.name == "Tool1")
                            laborSpawn.Spawn(Enumeration.LaborType.Axe);
                        else if (obj.name == "Tool2")
                            laborSpawn.Spawn(Enumeration.LaborType.Mine);
                        else
                            laborSpawn.Spawn(Enumeration.LaborType.Farm);
                    }
                    else
                    {
                        //not enough money alert
                        audioManager.PlayUISound(Enumeration.UISound.Denied);
                        UIManager.ShowResourceShortage("Not enough money");
                    }
                }
                else if (obj.CompareTag("ToSell")) //resource 판매 / 실패 시 경고 메시지
                {
                    if (obj.name == "Resource1")
                    {
                        if (!resourceManager.sellResource(Enumeration.ResourceType.Wood))
                        {
                            UIManager.ShowResourceShortage("Not enough woods to sell");
                            audioManager.PlayUISound(Enumeration.UISound.Denied);
                        }
                        else audioManager.PlayUISound(Enumeration.UISound.Coin);
                    }
                    else if (obj.name == "Resource2")
                    {
                        if (!resourceManager.sellResource(Enumeration.ResourceType.Stone))
                        {
                            UIManager.ShowResourceShortage("Not enough stones to sell");
                            audioManager.PlayUISound(Enumeration.UISound.Denied);
                        }
                        else audioManager.PlayUISound(Enumeration.UISound.Coin);
                    }
                    else
                    {
                        if (!resourceManager.sellResource(Enumeration.ResourceType.Food))
                        {
                            UIManager.ShowResourceShortage("Not enough foods to sell");
                            audioManager.PlayUISound(Enumeration.UISound.Denied);
                        }
                        else audioManager.PlayUISound(Enumeration.UISound.Coin);
                    }
                }
                else if (obj.CompareTag("LevelUp")) //levelup / 실패 시 경고 메시지
                {
                    if (obj.name == "Up1")
                    {
                        if (!resourceManager.upgrade(Enumeration.ResourceType.Wood))
                        {
                            UIManager.ShowResourceShortage("Not enough woods or money");
                            audioManager.PlayUISound(Enumeration.UISound.Denied);
                        }
                        else 
                        { 
                            laborSpawn.Spawn(Enumeration.LaborType.Hammer);
                            audioManager.PlayUISound(Enumeration.UISound.Coin);
                        }
                    }
                    else
                    {
                        if (!resourceManager.upgrade(Enumeration.ResourceType.Stone))
                        {
                            UIManager.ShowResourceShortage("Not enough stones or money");
                            audioManager.PlayUISound(Enumeration.UISound.Denied);
                        }
                        else
                        {
                            cannonManager.CannonUpgrade();
                            audioManager.PlayUISound(Enumeration.UISound.Coin);
                        }
                    }
                }
                else if (obj.CompareTag("Eat"))
                {
                    //hungry button에 저장된
                    if (resourceManager.feed())
                    {
                        obj.transform.parent.transform.parent.GetComponent<Labor>().GetFood();
                    }
                    else 
                    { 
                        UIManager.ShowResourceShortage("Not enough food");
                        audioManager.PlayUISound(Enumeration.UISound.Denied);
                    }
                }
                else if (obj.CompareTag("CannonPut"))
                {
                    GameObject newCannon = Instantiate(cannonPrefab, hit.point, Quaternion.LookRotation(hit.point-center));
                    if (cannons[currindex] != null) Destroy(cannons[currindex]);
                    cannons[currindex] = newCannon;
                    
                    currindex += 1;
                    if (currindex >= maxCannonNum) { currindex = 0; }
                }
                else
                {
                    Debug.Log(obj.name);
                }
            }
        }
    }
}