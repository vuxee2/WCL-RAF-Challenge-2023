using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class escCanvas : MonoBehaviour
{
    public GameObject options;
    public GameObject buttons;
    
    public void OptionsButton()
    {
        options.SetActive(true);
        buttons.SetActive(false);
    }
    public void ExitButton()
    {
        playerCam.isEscCanvasAcive = false;
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.LeaveRoom();
    }
}
