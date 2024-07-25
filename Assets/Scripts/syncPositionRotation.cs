using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class syncPositionRotation : MonoBehaviourPunCallbacks, IPunObservable
{
    Vector3 realPosition;
    Quaternion realRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {

        }
        else
        {
            transform.position = Vector3.Lerp (transform.position, realPosition, .1f);
            transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, .1f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream  stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
