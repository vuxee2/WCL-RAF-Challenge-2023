using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class endScreen : MonoBehaviourPunCallbacks
{
    public TMP_Text time;

    public static float startTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        time.text = (Time.time - startTime).ToString("F2") + " sec";

        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        foreach(GameObject player in players){
            player.SetActive(false);
        }
    }

    public void returnButton()
    {
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.LeaveRoom();
    }
}
