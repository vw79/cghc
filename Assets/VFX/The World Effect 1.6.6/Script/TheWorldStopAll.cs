using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class TheWorldStopAll : MonoBehaviour
{
    public MonoBehaviour[] ScriptsToStop;
    private bool isTimeStopped;
    private bool isStopping;

    private Vector3 OriginalRigidbodyVelocity;

    void Start()
    {

    }

    void Update()
    {
        isTimeStopped = GameObject.Find("The World Effect").GetComponent<TheWorldScript>().StopTime;

        if (isTimeStopped)
        {
            if (!isStopping)
            {
                isStopping = true;

                if (GetComponent<Rigidbody2D>() != null)
                {
                    OriginalRigidbodyVelocity = GetComponent<Rigidbody2D>().velocity;
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }

                foreach (MonoBehaviour script in ScriptsToStop)
                {
                    if(script.GetType() == typeof(Enemy_Fireball))
                    {
                        script.GetComponent<Enemy_Fireball>().isStopped = true;
                        continue;
                    }
                    script.enabled = false;
                }

                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().speed = 0;
                }
            }
        }
        else
        {
            if (isStopping)
            {
                isStopping = false;
                
                if (GetComponent<Rigidbody2D>() != null)
                {
                    GetComponent<Rigidbody2D>().velocity = OriginalRigidbodyVelocity;
                }

                foreach (MonoBehaviour script in ScriptsToStop)
                {
                    if (script.GetType() == typeof(Enemy_Fireball))
                    {
                        script.GetComponent<Enemy_Fireball>().isStopped = false;
                        continue;
                    }
                    script.enabled = true;
                }

                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().speed = 1;
                }
            }
        }
    }
}
