using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHP : MonoBehaviour
{
    public float TotalTime;
    public int HP;
    private UIManager manager;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        TotalTime = 0.0f;
        HP = 1000;
        manager = GameObject.Find("UIManager").GetComponent<UIManager>();
        manager.TextChange(Enumeration.ResourceType.HP, HP);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TotalTime += Time.deltaTime;
    }

    public void Restoration(int hp)
    {
        HP += hp;
        manager.TextChange(Enumeration.ResourceType.HP, HP);
    }

    public void WallBreak(int hp)
    {
        HP -= hp;
        manager.TextChange(Enumeration.ResourceType.HP, HP);
        if (HP < 0)
        {
            manager.TextChange(Enumeration.ResourceType.Time, (int)TotalTime);
            manager.GameOver();
            audioManager.GameOverSound();
            Time.timeScale = 0;
            manager.TextChange(Enumeration.ResourceType.HP, 0);
        }
    }
}
