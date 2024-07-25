using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class escPlayerCanvas : MonoBehaviour
{
    public GameObject escCanvasGO;

    public GameObject options;
    public GameObject buttons;

    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!view.IsMine) Destroy(escCanvasGO);
        else if(Input.GetKeyDown(KeyCode.Escape) && !options.activeSelf && !playerUI.isTutOpen)
        {
            escCanvasGO.SetActive(!escCanvasGO.activeSelf);
            if(escCanvasGO.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            Cursor.visible = escCanvasGO.activeSelf;
            playerCam.isEscCanvasAcive = escCanvasGO.activeSelf;

        }
        else if(Input.GetKeyDown(KeyCode.Escape)) 
        { 
            options.SetActive(false);
            buttons.SetActive(true);
        }
    }
   
}
