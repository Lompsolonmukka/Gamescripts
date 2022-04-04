using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public void newGameB(string MainMenu) //new game load level
    {

        SceneManager.LoadScene(MainMenu);
    }

    public void exitGameB() //close application
    {
        Application.Quit();
    }
}
