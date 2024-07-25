using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPitchChange : MonoBehaviour
{
    public AudioSource audioSource;
    // Update is called once per frame
    void FixedUpdate()
    {
        audioSource.pitch = 2f - ((scoreControl.treeHealth * 0.4f / 25f) + 0.8f);
    }
}

/*
OldRange = (OldMax - OldMin)  
NewRange = (NewMax - NewMin)  
NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin
*/
