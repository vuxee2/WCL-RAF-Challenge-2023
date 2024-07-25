using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class bomba : MonoBehaviourPun
{
    public GameObject explosionParticle;
    // Update is called once per frame

    PhotonView view;
    void Start()
    {
       view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if(view.IsMine && Input.GetKeyDown("q") && moneyManagement.moneyValue>=750)
        {
            FindObjectOfType<audioManager>().Play("bomba");

            this.photonView.RPC("destroy",RpcTarget.AllBuffered);

            int newMoney =  moneyManagement.moneyValue-750;
            view.RPC("changeMoney2",RpcTarget.AllBuffered,newMoney);
        }
    }
    [PunRPC]
    void destroy()
    {
        objectHold.holdingObject = false;
        cameraShake.explosionShake = true;

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Object");
        foreach(GameObject go in gos)
        {
            PhotonNetwork.Instantiate(explosionParticle.name,go.transform.position,Quaternion.identity);
            PhotonNetwork.Destroy(go);
        }
    }
    [PunRPC]
    void changeMoney2(int mon)
    {
        moneyManagement.moneyValue = mon;
        scoreControl.treeHealth_-=5;
    }
}
