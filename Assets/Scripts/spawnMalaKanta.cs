using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnMalaKanta : MonoBehaviour
{
    PhotonView view;
    public GameObject malaKanta;
    //public GameObject djubre;
    // Start is called before the first frame update
    void Start()
    {
       view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine && Input.GetKeyDown("e") && moneyManagement.moneyValue>=150)
        {
            FindObjectOfType<audioManager>().Play("malaKantaSpawn");

            PhotonNetwork.Instantiate(malaKanta.name,transform.position,Quaternion.identity);
            
            int newMoney =  moneyManagement.moneyValue-150;
            view.RPC("changeMoney1",RpcTarget.AllBuffered,newMoney);
        }
        /*if(Input.GetKeyDown("r") && view.IsMine)
        {
            PhotonNetwork.Instantiate(djubre.name,transform.position,Quaternion.identity);
        }*/
    }
    [PunRPC]
    void changeMoney1(int mon)
    {
       moneyManagement.moneyValue = mon;
       scoreControl.treeHealth_-=1;
    }
}
