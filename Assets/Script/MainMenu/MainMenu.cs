using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public GameObject mmenu;
	public GameObject smenu;
	
    void Start()
    {
        smenu.SetActive(false);
		Cursor.visible = true;
    }
	
	public void Settings()
	{
		mmenu.SetActive(false);
		smenu.SetActive(true);
	}

    public void Exit()
	{
		Application.Quit();
	}
}
