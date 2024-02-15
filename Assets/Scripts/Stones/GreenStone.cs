using Cinemachine;
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

    private CinemachineConfiner cinemachineConfiner;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        explosionCanvasGroup = GameObject.FindGameObjectWithTag("ExplosionFilter").GetComponent<CanvasGroup>();
        cinemachineConfiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void Start()
    {
        isM1 = true;
        c1.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.isGreen)
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
    }

    IEnumerator WaitTime(GameObject originalMap, GameObject targetMap)
    {
        yield return new WaitForSeconds(1f);
        originalMap.SetActive(false);
        targetMap.SetActive(true);

        GameObject background = GameObject.FindGameObjectWithTag("Background");

        if (background != null)
        {
            PolygonCollider2D polygonCollider = background.GetComponent<PolygonCollider2D>();
            if (polygonCollider != null)
            {
                cinemachineConfiner.m_BoundingShape2D = polygonCollider;

                cinemachineConfiner.InvalidatePathCache();
            }
        }

        StartCoroutine(GameManager.Instance.FadeOut(explosionCanvasGroup, 1f));
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
