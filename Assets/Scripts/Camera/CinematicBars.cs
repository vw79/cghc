using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CinematicBars : MonoBehaviour
{
    #region Varaibles
    #region CinematicBars
    private RectTransform topBar, bottomBar;
    private float changeSizeAmount;
    private float targetSize;
    private bool isActive;
    #endregion

    #region Cinemachine Zoom
    private Transform playerTransform;
    public CinemachineVirtualCamera virtualCamera; 
    public float zoomInSize = 5f; 
    public float zoomOutSize = 10f; 
    private float targetOrthoSize; 
    private float zoomChangeSpeed;
    private float originalScreenX;
    private float originalScreenY;
    #endregion
    #endregion

    private void Awake()
    {
        GameObject topBarGO = new GameObject("topBar", typeof(Image));
        topBarGO.transform.SetParent(transform,false);
        topBarGO.GetComponent<Image>().color = Color.black;
        topBar = topBarGO.GetComponent<RectTransform>();
        topBar.anchorMin = new Vector2(0, 1);
        topBar.anchorMax = new Vector2(1, 1);
        topBar.sizeDelta = new Vector2(0, 0);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject btmBarGO = new GameObject("btmBar", typeof(Image));
        btmBarGO.transform.SetParent(transform, false);
        btmBarGO.GetComponent<Image>().color = Color.black;
        bottomBar = btmBarGO.GetComponent<RectTransform>();
        bottomBar.anchorMin = new Vector2(0, 0);
        bottomBar.anchorMax = new Vector2(1, 0);
        bottomBar.sizeDelta = new Vector2(0, 0);

        if (virtualCamera != null)
        {
            var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (framingTransposer != null)
            {
                // Record the original values
                originalScreenX = framingTransposer.m_ScreenX;
                originalScreenY = framingTransposer.m_ScreenY;
            }
        }
    }

    private void Start()
    {
        if (virtualCamera != null && playerTransform != null)
        {
            virtualCamera.Follow = playerTransform;
        }
    }
    
    private void Update()
    {   
        if (isActive)
        {
            Vector2 sizeDelta = topBar.sizeDelta;
            sizeDelta.y += changeSizeAmount * Time.deltaTime;
            if (changeSizeAmount > 0)
            {
                if (sizeDelta.y >= targetSize)
                {
                    sizeDelta.y = targetSize;
                    isActive = false;
                }
            }
            else
            {
                if (sizeDelta.y <= targetSize)
                {
                    sizeDelta.y = targetSize;
                    isActive = false;
                }                
            }
            topBar.sizeDelta = sizeDelta;
            bottomBar.sizeDelta = sizeDelta;
        }

        if (isActive && virtualCamera != null)
        {
            float targetOrthoSize = targetSize > 0 ? zoomInSize : zoomOutSize;
            virtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize, targetOrthoSize, zoomChangeSpeed * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.Alpha0))
            { Hide(.3f); }

        if (Input.GetKeyDown(KeyCode.Alpha9))
            { Show(300, .3f); }
    }

    public void Show(float targetSize, float time) 
    {   
        GameManager.Instance.isCinematic = true;
        this.targetSize = targetSize;
        changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
        isActive = true;
        SetZoom(zoomInSize, time);
    }

    public void Hide(float time)
    {
        GameManager.Instance.isCinematic = false;
        targetSize = 0;
        changeSizeAmount = -topBar.sizeDelta.y / time;
        isActive = true;
        SetZoom(zoomOutSize, time);
    }

    private void SetZoom(float newSize, float time)
    {
        targetOrthoSize = newSize;
        if (virtualCamera != null)
        {
            zoomChangeSpeed = Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - newSize) / time;

            var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (framingTransposer != null)
            {
                if (newSize == zoomInSize)
                {
                    // Adjust for zoom in
                    framingTransposer.m_ScreenX = 0.2f;
                    framingTransposer.m_ScreenY = 0.75f;
                }
                else if (newSize == zoomOutSize)
                {
                    // Restore the original values when zooming out
                    framingTransposer.m_ScreenX = originalScreenX;
                    framingTransposer.m_ScreenY = originalScreenY;
                }
            }
        }
    }
}
