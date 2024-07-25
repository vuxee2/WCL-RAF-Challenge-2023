using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class destroyBoatColliders : MonoBehaviour
{
    public static bool shouldDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        shouldDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldDestroy)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
