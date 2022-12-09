using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labor : MonoBehaviour
{
    public string DestinationName;
    private Vector3 destination;
    public Vector3 direction;

    public string WorkSpaceName;
    private GameObject WorkSpace;

    //NavMeshAgent MyNavigation;
    Animator animator;
    GameObject hungryButton;
    GenerateResource GenerateResource;

    public bool walk, work, getFood;
    public float speed;
    public float distance;
    private float workTime = 0;
    private float getItemTime = 0;
    private int workNum = 0;

    private AudioSource audioSource;
    private WallHP hp;

    // Start is called before the first frame update
    void Start()
    {
        walk = true;
        work = false;
        getFood = false;

        destination = GameObject.Find(DestinationName).GetComponent<GetRandDestination>().randDest();
        direction = Vector3.Normalize(destination - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.LookRotation(direction);

        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

        WorkSpace = GameObject.Find(WorkSpaceName);
        if (WorkSpaceName != "WallHP") GenerateResource = WorkSpace.GetComponent<GenerateResource>();

        hungryButton = gameObject.transform.GetChild(1).gameObject;
        hungryButton.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Stop();

        hp = GameObject.Find("VariableManagers").transform.GetChild(1).GetComponent<WallHP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.HP < 0) audioSource.Stop();
        if (walk)
        {
            gameObject.transform.position += (direction * Time.deltaTime * speed);
            if (Vector3.Distance(destination, gameObject.transform.position) < distance)
            {
                walk = false;
                work = true;
                StartWork();
                
                animator.SetBool("walk", false);

                if (WorkSpaceName == "WallHP") gameObject.transform.rotation = Quaternion.identity;
            }
        }

        if (work)
        {
            workTime += Time.deltaTime;
            getItemTime += Time.deltaTime;

            if(getItemTime > 10)
            {
                if (WorkSpaceName != "WallHP") GenerateResource.GetItem(WorkSpaceName);
                else WorkSpace.GetComponent<WallHP>().Restoration(50);
                getItemTime = 0;
            }

            if(workTime > 31)
            {
                work = false;
                workTime = 0;
                animator.SetBool("idle", true);
                workNum++;

                hungryButton.SetActive(true);
                audioSource.Stop();
            }
        }

        if(!work && getFood)
        {
            animator.SetBool("idle", false);
            work = true;
            getFood = false;

            hungryButton.SetActive(false);
        }

        if (workNum >= 3) Destroy(gameObject);
    }

    public void GetFood()
    {
        getFood = true;
        StartWork();
    }

    public void StartWork()
    {
        audioSource.Play();
    }
}
