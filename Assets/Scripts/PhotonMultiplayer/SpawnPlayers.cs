using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }

}
