using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;

    [SerializeField] private AudioClip gunFire;
    [SerializeField] private AudioClip upgradedGunFire;
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip alienDeath;
    [SerializeField] private AudioClip marineDeath;
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip elevatorArrived;
    [SerializeField] private AudioClip powerUpPickup;
    [SerializeField] private AudioClip powerUpAppear;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        
        AudioSource[] sources = GetComponent<AudioSource[]>();
        foreach(AudioSource source in sources)
        {
            if(source.clip == null)
            {
                soundEffectAudio = source;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
