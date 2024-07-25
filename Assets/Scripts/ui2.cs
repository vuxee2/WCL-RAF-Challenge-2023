using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ui2 : MonoBehaviourPun
{
    public GameObject masterClientGO;
    public GameObject clientGO;

    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient && Input.GetKeyDown("f") && !started)
        {
            this.photonView.RPC("startGame",RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void startGame()
    {
        endScreen.startTime = Time.time;
        if(PhotonNetwork.IsMasterClient)
        {
            destroyBoatColliders.shouldDestroy = true;
            masterClientGO.SetActive(false);
            started = true;
            spawnObjects.stopSpawning = false;
            StartCoroutine(waitForTimerStart());
        }
        else
        {
            clientGO.SetActive(false);
            spawnObjects.stopSpawning = false;
            StartCoroutine(waitForTimerStart());
        }
    }
    IEnumerator waitForTimerStart()
    {
        yield return new WaitForSeconds(10);
        playerUI.startTimer = true;
    }
}
