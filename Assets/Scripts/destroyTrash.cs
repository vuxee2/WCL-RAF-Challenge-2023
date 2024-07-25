using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class destroyTrash : MonoBehaviourPun
{
    public GameObject destroyParticle;
    public GameObject destroyParticle2;

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Object" && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            FindObjectOfType<audioManager>().Play("trashDestroy");
            cameraShake.doShake = true;

            PhotonNetwork.Instantiate(destroyParticle.name,col.gameObject.transform.position,Quaternion.identity);
            PhotonNetwork.Destroy(col.gameObject);
            this.photonView.RPC("addMoney",RpcTarget.AllBuffered,Random.Range(10,20));
        }
        if(col.collider.tag == "MalaKanta" && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            FindObjectOfType<audioManager>().Play("charge");

            PhotonNetwork.Instantiate(destroyParticle2.name,col.gameObject.transform.position,Quaternion.identity);
            StartCoroutine(PlayAudioForMalaKanta(col.gameObject.GetComponent<malaKanta>().capacityValue));
            for(int i=0;i<col.gameObject.GetComponent<malaKanta>().capacityValue;i++)
            {
                this.photonView.RPC("addMoney",RpcTarget.AllBuffered,Random.Range(10,20));
            }
        }
    }

    [PunRPC]
    void addMoney(int value)
    {
        moneyManagement.moneyValue+=value;
    }

    IEnumerator PlayAudioForMalaKanta(int i)
    {
        if(i>0) FindObjectOfType<audioManager>().Play("trashDestroy");

        i--;
        yield return new WaitForSeconds(0.2f);
        if(i>0) StartCoroutine(PlayAudioForMalaKanta(i));
    }
   
}
