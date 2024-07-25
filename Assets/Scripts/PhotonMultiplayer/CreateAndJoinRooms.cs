using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public TMP_InputField nameInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
        PhotonNetwork.NickName = nameInput.text;
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
        PhotonNetwork.NickName = nameInput.text;
    }

    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
        PhotonNetwork.NickName = nameInput.text;
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Mapa1"); //za multiplayer scene
    }
}
