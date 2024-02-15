using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    private CanvasGroup explosionCanvasGroup;
    [SerializeField] private int SceneIndex;
    [SerializeField] private float delay;

    private void Awake()
    {
        explosionCanvasGroup = GameObject.FindGameObjectWithTag("ExplosionFilter").GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Teleporting to scene " + SceneIndex);
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DelayTeleport());
        }
    }

    private IEnumerator DelayTeleport()
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(GameManager.Instance.FadeIn(explosionCanvasGroup.GetComponent<CanvasGroup>(), 1f, SceneIndex));
    }
}
