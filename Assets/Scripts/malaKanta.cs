using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class malaKanta : MonoBehaviourPun
{
    public GameObject capacity;
    public GameObject waitText;

    public TMP_Text valueTXT;

    public int capacityValue = 0;

    public GameObject destroyParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        valueTXT.text = capacityValue.ToString() + "/5";
    }
    void LateUpdate()
    {
        capacity.transform.LookAt(capacity.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Object" && capacityValue < 5 && col.gameObject.GetComponent<PhotonView>().IsMine)
        {
            FindObjectOfType<audioManager>().Play("trashDestroy");
            cameraShake.doShake = true;

            PhotonNetwork.Instantiate(destroyParticle.name,col.gameObject.transform.position,Quaternion.identity);
            this.photonView.RPC("addCapacity",RpcTarget.AllBuffered);
            PhotonNetwork.Destroy(col.gameObject);
        }
        if(col.collider.tag == "velikaKanta")
        {
            this.photonView.RPC("resetCapacity",RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    void addCapacity()
    {
        capacityValue++;
    }
    [PunRPC]
    void resetCapacity()
    {
        StartCoroutine(WaitForReset());
    }
    IEnumerator WaitForReset()
    {
        waitText.SetActive(true);
        yield return new WaitForSeconds(1);
        waitText.SetActive(false);
        capacityValue = 0;
    }
}
