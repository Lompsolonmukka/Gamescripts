using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mouselock : MonoBehaviour
{
    static private Scene scene;  
    public bool pause;
    public GameObject pauseMenu;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = scene.name;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            if (pause) //activate pausemenu, release mouse, pause game
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pause = true;
            }

            if (!pause) //deactivate pausemenu and hide mouse, activate game
            {
                pause = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if ((sceneName == "MainMenu") || (pause)) //if menu  is loaded, unlock mouse
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
