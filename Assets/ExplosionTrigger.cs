using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    private GameObject explosion;
    private CinematicBars cinematicBars;   

    private void Awake()
    {
        explosion = GameObject.Find("Explosion");
        cinematicBars = GameObject.Find("CinematicBars").GetComponent<CinematicBars>();
    }

    private void Start()
    {
        explosion.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {       
            GameManager.Instance.DisableControl();
            explosion.SetActive(true);
            cinematicBars.Show(300, .3f);
        }   
    }
}
