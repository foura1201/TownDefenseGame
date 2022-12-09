using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject UIObj;
    private AudioSource UI;

    public AudioClip BGMClip;
    public AudioClip GameOverClip;
    public AudioClip PurchaseClip;
    public AudioClip PurchaseFailClip;
    public AudioClip PhaseUpClip;
    public AudioClip LaborSpawnClip;

    private AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        UI = UIObj.GetComponent<AudioSource>();
        BGM.clip = BGMClip;
        BGM.loop = true;
        BGM.Play();
        UI.loop = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverSound()
    {
        BGM.loop = false;
        BGM.clip = GameOverClip;
        BGM.Play();
    }

    public void PlayUISound(Enumeration.UISound sound)
    {
        UI.Stop();

        if (sound == Enumeration.UISound.Coin)
            UI.clip = PurchaseClip;
        if (sound == Enumeration.UISound.Denied)
            UI.clip = PurchaseFailClip;
        if (sound == Enumeration.UISound.LaborSpawn)
            UI.clip = LaborSpawnClip;
        if (sound == Enumeration.UISound.PhaseUp)
            UI.clip = PhaseUpClip;

        UI.Play();
    }
}