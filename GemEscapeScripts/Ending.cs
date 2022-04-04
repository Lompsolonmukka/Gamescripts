using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject player;
    public PlayerController winner;
    
    private bool isPlayerExit; //check if player reached the exit
    public Text notText;
    public Text WinText;

    private float timer;
    public float fadeDuration = 1f; //screen fade
    public float displayDuration = 2f; //how long the picture will stay before restart
    public CanvasGroup WinnerCanvasGroup; //canvas for the images if win game

    private void OnTriggerEnter(Collider other) //colliding with the exit door trigger
    {
        if ((other.gameObject == player) && (winner.winCheck)) //if the colliding object is player change player exit true
        {
            isPlayerExit = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (winner.winCheck)
        {
            notText.text = "";
        }

        if (isPlayerExit) //UPDATE THIS: if win, show winning picture or scene
        {
            player.GetComponent<PlayerController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WinText.text = "";
            WinLevel();
        }
    }

    public void WinLevel()
    {
        timer += Time.deltaTime; //timer equal to itself plus deltatime
        WinnerCanvasGroup.alpha = timer / fadeDuration; //set the alpha on canvas group

        if (timer > fadeDuration + displayDuration)
        {
            SceneManager.LoadScene("MainMenu"); //go to main menu
        }
    }
}
