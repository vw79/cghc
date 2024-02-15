using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenStone : MonoBehaviour
{
    public GameObject m1;
    public GameObject c1;
    public GameObject m2;
    private GameObject player;
    public GameObject Lv5Tu;
    private Rigidbody2D rb;
    private CanvasGroup explosionCanvasGroup;
    private bool isC1;
    private bool isM1;
    private bool isM2;
    public bool canUse;

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
        if (GameManager.Instance.isLava) return;

        isM1 = true;
        Lv5Tu.SetActive(false);
        m1.SetActive(true);
        c1.SetActive(false);
        m2.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.isGreen)
        {
            Lv5Tu.SetActive(true);

            if (isM1 && Input.GetKeyDown(KeyCode.E) && canUse)
            {
                c1.SetActive(false);
                rb.bodyType = RigidbodyType2D.Static;
                StartCoroutine(GameManager.Instance.FadeInNoTrans(explosionCanvasGroup, 1f));
                isM1 = false;
                isC1 = true;
                StartCoroutine(WaitTime(m1, c1));
                canUse = false;
            }
            else if (isC1 && Input.GetKeyDown(KeyCode.E) && canUse)
            {
                rb.bodyType = RigidbodyType2D.Static;
                StartCoroutine(GameManager.Instance.FadeInNoTrans(explosionCanvasGroup, 1f));
                isM2 = true;
                isC1 = false;
                GameManager.Instance.isLava = true;
                StartCoroutine(WaitTime(c1, m2));
                canUse = false;
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
