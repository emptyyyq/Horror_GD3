using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scream : MonoBehaviour
{
    public AudioSource myFx;

    public AudioClip ClipFx;

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("screamer") && other.CompareTag("Player"))
        {

            myFx.PlayOneShot(ClipFx);
        }
    }
}
