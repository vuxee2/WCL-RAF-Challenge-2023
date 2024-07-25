using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class destroy2s : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(destroy());
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.Destroy(gameObject);
    }
}
