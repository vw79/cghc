using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject mmenu;
	public GameObject smenu;
	public GameObject vmenu;
	public GameObject dmenu;
	public GameObject pmenu;
	
    void Start()
    {
        smenu.SetActive(false);
		vmenu.SetActive(false);
		dmenu.SetActive(false);
		Cursor.visible = true;
        PlayerPrefs.SetInt("LastSceneIndex", SceneManager.GetActiveScene().buildIndex);
    }

	public void Continue()
	{
        int lastSceneIndex = PlayerPrefs.GetInt("LastSceneIndex", 0); // Use a default value of 0 or your main menu index
        SceneManager.LoadScene(lastSceneIndex);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Settings()
	{
		mmenu.SetActive(false);
		smenu.SetActive(true);
	}
	
	public void Display()
	{
		smenu.SetActive(false);
		dmenu.SetActive(true);
	}
	
	public void Volume()
	{
		smenu.SetActive(false);
		vmenu.SetActive(true);
	}
	
	public void Return()
	{
		mmenu.SetActive(true);
		smenu.SetActive(false);
	}
	
	public void sReturn()
	{
		smenu.SetActive(true);
		dmenu.SetActive(false);
		vmenu.SetActive(false);
	}
	
	public void gReturn()
	{
		pmenu.SetActive(false);
		Time.timeScale = 1;
	}
	
	public void BTMM()
	{
		SceneManager.LoadScene("MainMenu");
	}

    public void Exit()
	{
		Application.Quit();
	}
}
