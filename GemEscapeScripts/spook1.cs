using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spook1 : MonoBehaviour
{
    public AudioClip spook;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(spook, transform.position);
        }
    }
}
