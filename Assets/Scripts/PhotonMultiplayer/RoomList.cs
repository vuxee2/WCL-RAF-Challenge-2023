using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject RoomPrefab;
    public GameObject[] AllRooms;
  
    public override void OnRoomListUpdate(List<RoomInfo> roomlist)
    {
        for(int i=0;i<AllRooms.Length;i++)
        {
            Destroy(AllRooms[i]);
        }

        AllRooms = new GameObject[roomlist.Count];


        for(int i=0;i<roomlist.Count;i++)
        {
            if(roomlist[i].IsOpen && roomlist[i].IsVisible && roomlist[i].PlayerCount >= 1)
            {
                GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                Room.GetComponent<Room>().Name.text = roomlist[i].Name;
                Room.GetComponent<Room>().Players.text = roomlist[i].PlayerCount.ToString();

                AllRooms[i] = Room;
            }
        }
    }
}
