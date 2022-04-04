using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    private CharacterController cc; //calling character controller from unity
    public AudioClip step; //assign audiofile for stepping
    AudioSource auso; //audiosource for codes to play the sound

    // Calling components in the start
    void Start()
    {
        cc = GetComponent<CharacterController>();
        auso = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && !auso.isPlaying) //if player is on ground, has movement greater than 2 and the audio is not already playing
        {
            auso.clip = step; //audiosource and clip
            auso.volume = Random.Range(0.8f, 1); //change volume every time sound is played
            auso.pitch = Random.Range(0.8f, 1.1f); //change pitch also every time sound is played
            auso.Play(); //play sound.
        }
    }
}
