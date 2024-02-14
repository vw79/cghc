using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenStone : MonoBehaviour
{
    public GameObject m1;
    public GameObject c1;
    private GameObject player;
    private Rigidbody2D rb;
    private CanvasGroup explosionCanvasGroup;
    private bool isC1;
    private bool isM1;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        explosionCanvasGroup = GameObject.FindGameObjectWithTag("ExplosionFilter").GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        isM1 = true;
        c1.SetActive(false);
    }

    private void Update()
    {
        if (isM1 && Input.GetKeyDown(KeyCode.E))
        {
            rb.bodyType = RigidbodyType2D.Static;           
            StartCoroutine(GameManager.Instance.FadeInNoTrans(explosionCanvasGroup, 1f)); 
            isM1 = false;
            isC1 = true;
            StartCoroutine(WaitTime(m1, c1));
        }
        else if (isC1 && Input.GetKeyDown(KeyCode.E))
        {
            rb.bodyType = RigidbodyType2D.Static;           
            StartCoroutine(GameManager.Instance.FadeInNoTrans(explosionCanvasGroup, 1f));
            isM1 = true;
            isC1 = false;
            StartCoroutine(WaitTime(c1, m1));
        }
    }

    IEnumerator WaitTime(GameObject originalMap, GameObject targetMap)
    {
        yield return new WaitForSeconds(1f);
        originalMap.SetActive(false);
        targetMap.SetActive(true);
        StartCoroutine(GameManager.Instance.FadeOut(explosionCanvasGroup, 1f));
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
