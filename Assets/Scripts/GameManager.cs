using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameObject loseMenu;
    private GameObject winMenu;
    private GameObject pauseMenu;

    private GameObject player;
    private PlayerHealthSystem playerHealth;
    private Transform spawn1;

    
    private PlayerMovement playerMovement;  
    private PlayerAttack playerAttack;
    private Anim anim;
    public bool isPaused;
    public bool isCinematic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        loseMenu = GameObject.FindGameObjectWithTag("GameOver");
        //winMenu = GameObject.Find("WinMenu");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(player);

        playerHealth = player.GetComponent<PlayerHealthSystem>();
        spawn1 = GameObject.FindWithTag("InitialSpawn").transform;

        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttack = player.GetComponent<PlayerAttack>();
        anim = player.GetComponentInChildren<Anim>();

        loseMenu.SetActive(false);
        //winMenu.SetActive(false);
        pauseMenu.SetActive(false);
        SpawnPlayer();
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
    }

    void SpawnPlayer()
    {
        player.transform.position = spawn1.position;
    }

    public void PlayerDied()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayerWon()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        player.SetActive(true);
        player.transform.position = spawn1.position;
        playerHealth.ResetHealth();

        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        playerAttack.enabled = true;
        playerMovement.enabled = true;
        anim.enabled = true;

        isPaused = false;
    }

    #region PauseMenu
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playerAttack.enabled = false;
        playerMovement.enabled = false;
        anim.enabled = false;
        playerHealth.enabled = false;
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playerAttack.enabled = true;
        playerMovement.enabled = true;
        playerHealth.enabled = true;
        anim.enabled = true;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
