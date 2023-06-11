using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void Start()
    {

    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_V0");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
