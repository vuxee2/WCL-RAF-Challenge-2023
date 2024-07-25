using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class playerUI : MonoBehaviour
{
    public Image fillbar;
    public TMP_Text fillbarTXT;

    public TMP_Text players;

    public GameObject masterClientGO;
    public GameObject clientGO;

    public TMP_Text spawnRateTXT;

    public TMP_Text timerTXT;
    public static int timer = 5;
    public static bool startTimer;

    PhotonView view;

    public bool started = false;

    public Camera cam;

    public GameObject Crosshair1;
    public GameObject Crosshair2;

    public GameObject tutorialText;

    public static bool isTutOpen = false;

    void Start()
    {
        view = GetComponent<PhotonView>();

        if(PhotonNetwork.IsMasterClient)
        {
            masterClientGO.SetActive(true);
            clientGO.SetActive(false);
        }
        else
        {
            masterClientGO.SetActive(false);
            clientGO.SetActive(true);
        }

        
    }
    void Update()
    {
        if(!view.IsMine) Destroy(gameObject);

        spawnRateTXT.text = spawnObjects.spawnRate.ToString() + "/1 sec";

        players.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();

        fillbar.fillAmount = (float)scoreControl.treeHealth*(100/scoreControl.treeHealth_)/100;
        fillbarTXT.text = scoreControl.treeHealth.ToString() + "/" + scoreControl.treeHealth_.ToString();

        timerTXT.text = timer.ToString();

        WhatImLooking();

        if(startTimer){
            StartCoroutine(timerMinusMinus());
            startTimer = false;
        }

        if(view.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && tutorialText.activeSelf)
            {
                tutorialText.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        isTutOpen = tutorialText.activeSelf;
    }



    void WhatImLooking()
    {
        var ray = cam.ViewportPointToRay ( new Vector3(0.5f,0.5f,0f));

        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 2.6f)) 
        {
            //Debug.Log("I'm looking at " + hit.transform.tag);
            //Debug.DrawLine (ray.origin, hit.point);
            if(hit.transform.tag == "Object")
            {
                Crosshair1.SetActive(false);
                Crosshair2.SetActive(true);
            }
            else
            {
                Crosshair1.SetActive(true);
                Crosshair2.SetActive(false);
            }
            if(hit.transform.tag == "book")
            {
                if(view.IsMine)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        tutorialText.SetActive(true);
                    }
                }
            }
        } 
        else 
        {
            //Debug.Log("I'm looking at nothing!");
            Crosshair1.SetActive(true);
            Crosshair2.SetActive(false);
        }
    }

    IEnumerator timerMinusMinus()
    {
        yield return new WaitForSeconds(1);
        timer--;
        StartCoroutine(timerMinusMinus());
    }
}
