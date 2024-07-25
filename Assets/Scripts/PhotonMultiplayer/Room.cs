using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Players;
    
    public void JoinRoom()
    {
        GameObject.Find("CreateAndJoinRooms").GetComponent<CreateAndJoinRooms>().JoinRoomInList(Name.text);
    }
}
