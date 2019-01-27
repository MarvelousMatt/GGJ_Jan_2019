using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void Quit()
    {
        Application.Quit();
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("SampleScene 1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
