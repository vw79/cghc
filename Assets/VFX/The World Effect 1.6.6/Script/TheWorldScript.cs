using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorldScript : MonoBehaviour
{
    public bool StopTime;
    public int time;

    [SerializeField] private TheWorldResources TheWorldResource;
    [SerializeField] private Cooldown cooldownUI;

    int MaxStopTime;
    int EndSoundEffectPlayTime;
    AudioClip[] TheWorldSoundEffects = new AudioClip[2];

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (cooldownUI.UseSpell())
            {
                StartTimeStop();
            }
        }
        MaxStopTime = TheWorldResource.MaxStopTime;
        EndSoundEffectPlayTime = TheWorldResource.EndSoundEffectPlayTime;
        TheWorldSoundEffects[0] = TheWorldResource.TheWorldSoundEffects[0];
        TheWorldSoundEffects[1] = TheWorldResource.TheWorldSoundEffects[1];
    }

    void StartTimeStop()
    {
        if (!StopTime)
        {
            GetComponent<AudioSource>().PlayOneShot(TheWorldSoundEffects[0]);
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                ParticleSystem particleSystem;
                particleSystem = transform.GetChild(0).GetChild(i).GetComponent<ParticleSystem>();
                particleSystem.Play();
            }
            
            for (int i = 0; i < transform.GetChild(2).childCount; i++)//stand power
            {
                ParticleSystem particleSystem;
                particleSystem = transform.GetChild(2).GetChild(i).GetComponent<ParticleSystem>();
                var main = particleSystem.main;
                main.duration = MaxStopTime;
                main.startLifetime = MaxStopTime;

                particleSystem.Play();
            }

            StopTime = true;

            time = MaxStopTime;
            StartCoroutine("StartStopTime");
        }
    }

    public IEnumerator StartStopTime()
    {
        while (time > 0)
        {
            time--;
            if (time <= 0)
            {
                StopTime = false;

                yield break;
            }
            else if (time > 0)
            {
                if (time == EndSoundEffectPlayTime)
                {
                    ClearTimeStop();
                }

                if (time == 1)
                {
                    EndParticleSystemEffect();
                }
                yield return new WaitForSecondsRealtime(1);
            }
        }
    }

    void ClearTimeStop()
    {
        GetComponent<AudioSource>().PlayOneShot(TheWorldSoundEffects[1]);
    }

    void EndParticleSystemEffect()
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            ParticleSystem particleSystem;
            particleSystem = transform.GetChild(1).GetChild(i).GetComponent<ParticleSystem>();
            particleSystem.Play();
        }
    }
}
