using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SlimeGenerators;
    public GameObject TurtleShellGenerators;
    private UIManager manager;
    List<EnemyGenerator> SlimeGenerator = new List<EnemyGenerator>();
    List<EnemyGenerator> TurtleShellGenerator = new List<EnemyGenerator>();

    public float SlimeGenerationTime;
    public float TurtleShellGenerationTime;
    private int SlimeCount;
    private int TurtleShellCount;

    public int phaseVar;

    private float time = 0;
    public float PhaseUpTime;

    private AudioManager audioManager;

    void Start()
    {
        manager = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        SlimeCount = SlimeGenerators.transform.childCount;
        TurtleShellCount = TurtleShellGenerators.transform.childCount;

        for (int i = 0; i < SlimeCount; i++)
            SlimeGenerator.Add(SlimeGenerators.transform.GetChild(i).GetComponent<EnemyGenerator>());

        for (int i = 0; i < TurtleShellCount; i++)
            TurtleShellGenerator.Add(TurtleShellGenerators.transform.GetChild(i).GetComponent<EnemyGenerator>());

        for (int i = 0; i < SlimeCount; i++)
            SlimeGenerator[i].SetGenTime(SlimeGenerationTime);

        for (int i = 0; i < TurtleShellCount; i++)
            TurtleShellGenerator[i].SetGenTime(TurtleShellGenerationTime);

        SlimeGenerators.SetActive(false);
        TurtleShellGenerators.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > PhaseUpTime)
        {
            time = 0;
            phaseVar++;
            manager.PhaseChange(phaseVar);
            audioManager.PlayUISound(Enumeration.UISound.PhaseUp);

            SlimeGenerationTime *= 0.9f;

            // generation time set
            for (int i = 0; i < SlimeCount; i++)
                SlimeGenerator[i].SetGenTime(SlimeGenerationTime);

            if (phaseVar >= 5)
            {
                TurtleShellGenerationTime *= 0.9f;
                for (int i = 0; i < TurtleShellCount; i++)
                    TurtleShellGenerator[i].SetGenTime(TurtleShellGenerationTime);
            }

            //setActive
            if (phaseVar == 1)
            {
                SlimeGenerators.SetActive(true);
                RandomChoice(Enumeration.MonsterType.Slime, 2 * phaseVar);
            }

            //random setActive
            else if (phaseVar <= 4)
            {
                RandomChoice(Enumeration.MonsterType.Slime, 2 * phaseVar);
            }
            else if (phaseVar == 5)
            {
                for (int i = 0; i < SlimeCount; i++)
                {
                    SlimeGenerator[i].gameObject.SetActive(true);
                }
                TurtleShellGenerators.SetActive(true);
                RandomChoice(Enumeration.MonsterType.TurtleShell, 2 * (phaseVar - 4));
            }
            else if (phaseVar <= 6)
            {
                RandomChoice(Enumeration.MonsterType.TurtleShell, 2 * (phaseVar - 4));
            }
            else if (phaseVar == 7)
            {
                for (int i = 0; i < TurtleShellCount; i++)
                {
                    TurtleShellGenerator[i].gameObject.SetActive(true);
                }
            }
        }
    }

    private void RandomChoice(Enumeration.MonsterType monster, int n)
    {
        if (monster == Enumeration.MonsterType.Slime)
        {
            bool[] index = new bool[SlimeCount];
            for (int i = 0; i < SlimeCount; i++)
            {
                index[i] = false;
            }

            for (int i = 0; i < n; i++)
            {
                int k = Random.Range(0, SlimeCount);
                while (index[k]) k = Random.Range(0, SlimeCount);
                index[k] = true;
            }

            for (int i = 0; i < SlimeCount; i++)
            {
                SlimeGenerator[i].gameObject.SetActive(index[i]);
            }
        }
        else
        {
            bool[] index = new bool[TurtleShellCount];
            for (int i = 0; i < TurtleShellCount; i++)
            {
                index[i] = false;
            }

            for (int i = 0; i < n; i++)
            {
                int k = Random.Range(0, TurtleShellCount);
                while (index[k]) k = Random.Range(0, TurtleShellCount);
                index[k] = true;
            }

            for (int i = 0; i < TurtleShellCount; i++)
            {
                TurtleShellGenerator[i].gameObject.SetActive(index[i]);
            }
        }
    }
}