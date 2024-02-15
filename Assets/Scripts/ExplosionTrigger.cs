using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExplosionTrigger : MonoBehaviour
{
    private GameObject explosion;
    private CinematicBars cinematicBars;
    private CanvasGroup explosionCanvasGroup;

    private void Awake()
    {
        explosion = GameObject.Find("Explosion");
        cinematicBars = GameObject.Find("CinematicBars").GetComponent<CinematicBars>();
        explosionCanvasGroup = GameObject.FindGameObjectWithTag("ExplosionFilter").GetComponent<CanvasGroup>();
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

            StartCoroutine(DelayExplosionEffect(3f));
        }
    }

    private IEnumerator DelayExplosionEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        explosion.SetActive(false);
        StartCoroutine(GameManager.Instance.FadeIn(explosionCanvasGroup.GetComponent<CanvasGroup>(), 1f, 4));
    }
}
