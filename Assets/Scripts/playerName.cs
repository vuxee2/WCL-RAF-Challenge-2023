using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class playerName : MonoBehaviourPunCallbacks
{
    public TMP_Text PLAYERname;
    // Start is called before the first frame update
    void Start()
    {
        PLAYERname.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
