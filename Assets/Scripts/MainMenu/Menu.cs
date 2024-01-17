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
