using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f; //Player speed
    public float sensitivity = 100.0f; //mouse sensitivity
    CharacterController character;
    public GameObject cam; //player camera
    float moveFB, moveLR;
    float rotX, rotY;
    float gravity = -9.8f;

    public AudioClip breakCollect;
    public AudioClip collectable;
    public AudioClip booster;
    public bool winCheck;

    public Text countText;
    public int count;
    public Text winText;
    public Text boostText;
    public Text notText;

    public Camera miniMapCam;

    public GameObject Lamp = GameObject.FindGameObjectWithTag("lights");


    void Start()
    {
        character = GetComponent<CharacterController>();
        

        //set all texts invisible until needed
        count = 0;
        SetCountText();
        winText.text = "";
        boostText.text = "";
        winCheck = false;
        notText.text = "";

    }

    void Update()
    {
        moveFB = Input.GetAxis("Horizontal") * speed;
        moveLR = Input.GetAxis("Vertical") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 movement = new Vector3(moveFB, gravity, moveLR);

        CameraRotation(cam, rotX, rotY);

        movement = transform.rotation * movement;
        character.Move(movement * Time.deltaTime);

        if ((Input.GetKeyDown(KeyCode.B)) && ((Input.GetKeyDown(KeyCode.N))))
        {
            count = 199;
            SetCountText();
        }
    }


    void CameraRotation(GameObject cam, float rotX, float rotY)
    {
        transform.Rotate(0, rotX * Time.deltaTime, 0);
        cam.transform.Rotate(0, 0, 0);
    }


    //ALL WORKS
    void OnTriggerEnter(Collider other) //entering triggers with tags
    {
        if (other.gameObject.CompareTag("Pick Up")) //trigger gem, deactivates object and adds one to count variable, calls setCountText so shows right amount of gems in HUD
        {
            AudioSource.PlayClipAtPoint(collectable,transform.position);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Show Enemy")) //trigger boostGem, activates enemy dots on minimap for 60 seconds and shows text
        {
            AudioSource.PlayClipAtPoint(booster, transform.position); //play sound of pickup at player location
            boostText.text = "All enemies now show on minimap for 60 seconds!";
            other.gameObject.SetActive(false); //deactivate object
            boostText.CrossFadeAlpha(0.0f, 5, false); //fade out text after 5 seconds
            StartCoroutine(ShowEnemy());
        }
        if (other.gameObject.CompareTag("Exit")) //triggers exit, if not all gems collected, shows info text
        {
            notText.text = "Door is locked! You need to collect all the gems to exit the level!";
        }
        if (other.gameObject.CompareTag("break")) //
        {
            AudioSource.PlayClipAtPoint(breakCollect, transform.position);
            other.gameObject.SetActive(false);
            Lamp.gameObject.SetActive(false);
            
        }
    }

    //WORKS
    private void OnTriggerExit(Collider other) //when leaving Exit door trigger, info text disappears
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            notText.text = "";
        }
    }

    //WORKS AS INTENDED
    void SetCountText() //checks if all gems are collected and shows winning text on screen and changes winCheck to true
    {
        countText.text = "Gems collected: " + count.ToString() + " /200";
        if (count >= 200)
        {
            winCheck = true;
            winText.text = "You collected all the gems! Run to the exit!";
        }
    }


    //THIS WORKS AS INTENDED
    IEnumerator ShowEnemy() //coroutine that shows enemy pointers on minimap for 60 seconds
    {
        miniMapCam.cullingMask |= 1 << LayerMask.NameToLayer("minimap"); //activate minimap layer in minimapcam

        yield return new WaitForSeconds(60);

        miniMapCam.cullingMask &= ~(1 << LayerMask.NameToLayer("minimap")); //deactivate layer
    }
}

