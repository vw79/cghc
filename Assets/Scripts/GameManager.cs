using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameObject vCamera;
    private CinemachineConfiner cinemachineConfiner;

    private GameObject inGameUI;

    public GameObject loseMenu;
    public GameObject HUD;
    private GameObject pauseMenu;
    private GameObject explosionFilter;
    private CanvasGroup explosionCanvasGroup;
    private CinematicBars cinematicBars;
    private Dialogue dialogue;

    private Vector3 spawnPosition;
    private GameObject player;
    private PlayerHealthSystem playerHealth;
    private PlayerShoot playerShoot;
  
    private PlayerMovement playerMovement;  
    private PlayerAttack playerAttack;
    private TheWorldScript theWorldScript;
    public bool isPaused;
    public bool isCinematic;

    public bool isBlue;
    public bool isRed;
    public bool isGreen;

    public Image blueImage;
    public Image redImage;
    public Image greenImage;

    public Image teleportImage;
    public Image teleportFrame;
    public Image zawarudoImage;
    public Image zawarudoFrame;

    public GameObject bgm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return; 
        }

        cinematicBars = GameObject.Find("CinematicBars").GetComponent<CinematicBars>();
        loseMenu = GameObject.FindGameObjectWithTag("GameOver");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        explosionFilter = GameObject.FindGameObjectWithTag("ExplosionFilter");
        explosionCanvasGroup = explosionFilter.GetComponent<CanvasGroup>();
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        vCamera = GameObject.FindGameObjectWithTag("Camera");
        cinemachineConfiner = FindObjectOfType<CinemachineConfiner>();
        inGameUI = GameObject.FindGameObjectWithTag("UI");

        DontDestroyOnLoad(explosionCanvasGroup);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(vCamera);
        DontDestroyOnLoad(inGameUI);
        DontDestroyOnLoad(bgm);


        playerHealth = player.GetComponent<PlayerHealthSystem>();

        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerShoot = player.GetComponent<PlayerShoot>();
        theWorldScript = player.GetComponentInChildren<TheWorldScript>();

        loseMenu.SetActive(false);
        pauseMenu.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        Cursor.visible = false;
        explosionCanvasGroup.alpha = 0;
        blueImage.enabled = false;
        redImage.enabled = false;
        greenImage.enabled = false;
        teleportImage.enabled = false;
        teleportFrame.enabled = false;
        zawarudoImage.enabled = false;
        zawarudoFrame.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            playerAttack.enabled = false;
        }
        else
        {
            playerAttack.enabled = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            theWorldScript.enabled = true;
            zawarudoImage.enabled = true;
            zawarudoFrame.enabled = true;
        }
        else
        {
            theWorldScript.enabled = false;
            zawarudoImage.enabled = false;
            zawarudoFrame.enabled = false;
        }

        if(isRed)
        {
            playerShoot.enabled = true;
            teleportImage.enabled = true;
            teleportFrame.enabled = true;
        }
        else
        {
            playerShoot.enabled = false;
            teleportImage.enabled = false;
            teleportFrame.enabled = false;
        }
    }

    public void PlayerDied()
    {   
        Cursor.visible = true;
        isPaused = true;
        DisableControl();
        loseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Cursor.visible = false;
        explosionCanvasGroup.alpha = 0;
        player.SetActive(true);
        EnableControl();
        playerHealth.ResetHealth();

        player.transform.position = spawnPosition; 

        loseMenu.SetActive(false);
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        if (dialogue == null) return;
        else { dialogue.HideAll(); }
        
        StartCoroutine(FadeOut(explosionCanvasGroup.GetComponent<CanvasGroup>(), 1f));
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
    
        Transform spawn1 = GameObject.FindGameObjectWithTag("InitialSpawn").transform;
        spawnPosition = spawn1.position;
        player.transform.position = spawn1.position;
        
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }


    #region Control Management
    public void DisableControl()
    {
        playerAttack.enabled = false;
        playerMovement.enabled = false;
        playerHealth.enabled = false;
        playerShoot.enabled = false;
    }

    public void EnableControl()
    {
        playerAttack.enabled = true;
        playerMovement.enabled = true;
        playerHealth.enabled = true;
        playerShoot.enabled = true;
    }
    #endregion

    #region FadeIn and FadeOut Coroutines
    public IEnumerator FadeIn(CanvasGroup canvasGroup, float duration, int sceneIndex)
    {
        DisableControl();
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;

        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public IEnumerator FadeInNoTrans(CanvasGroup canvasGroup, float duration)
    {
        DisableControl();
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;

        cinematicBars.Hide(0.3f);
    }
    #endregion

    #region PauseMenu
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        DisableControl();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        EnableControl();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        Destroy(bgm);
        Destroy(vCamera);
        Destroy(explosionFilter);
        Destroy(player);        
        Destroy(inGameUI);       
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);       
        Cursor.visible = true;
        Destroy(gameObject);
    }

    public void CinematicChat()
    {
        DisableControl();
        Time.timeScale = 0f;
    }
    #endregion
}
