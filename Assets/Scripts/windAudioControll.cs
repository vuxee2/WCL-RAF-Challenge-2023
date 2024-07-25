using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windAudioControll : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource.volume = 0;
        audioSource.pitch = 0;
    }
    void FixedUpdate()
    {
        audioSource.pitch = playerMovement.moveSpeed / 10 - 0.6f;
        audioSource.volume = playerMovement.moveSpeed / 20 - 0.6f;
    }
}
