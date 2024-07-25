using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class objectHold : MonoBehaviour
{
    public Transform Player;
    private GameObject Object;
    public Transform PlayerTransform;
    public float range = 3;
    public float Go = 100f;
    public Camera Camera;
    public Transform camTransform;

    private Rigidbody rb;

    public static bool holdingObject;

    public KeyCode holdKey = KeyCode.Mouse0;
    public KeyCode throwKey = KeyCode.Mouse1;

    private string hitTag;

    PhotonView view;
    void  Start() {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if(view.IsMine)
        {
            if (Input.GetKey(holdKey))
            {
                StartPickUp();
            }

            if (Input.GetKeyUp(holdKey))
            {
                holdingObject = false;
                Drop();
            }

            if(Input.GetKeyDown(throwKey))
            {
                Throw();
            }
        }
        
    }

    void StartPickUp ()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            if ((hit.transform.tag == "Object" || hit.transform.tag == "MalaKanta") && !holdingObject)
            {
                hitTag = hit.transform.tag;

                Object = hit.transform.gameObject;
                rb = Object.GetComponent<Rigidbody>();
                PickUp();
            }
        }
    }

    void PickUp ()
    {
        Object.GetComponent<Collider>().enabled=false;

        holdingObject = true;

        if(hitTag == "Object")
        {
            FindObjectOfType<audioManager>().Play("grab");

            Object.transform.SetParent(PlayerTransform);
            Object.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            Object.transform.localPosition = Vector3.zero;
        }
        else
        {
            FindObjectOfType<audioManager>().Play("malaKantaGrab");

            Object.transform.SetParent(PlayerTransform);
            Object.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //Object.transform.localPosition = Vector3.zero;
        }

        Object.GetComponent<Rigidbody>().useGravity = false;
        Object.GetComponent<Rigidbody>().drag = 100f;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY| RigidbodyConstraints.FreezeRotationZ | 
        RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezePositionZ;

    }

    void Drop ()
    {
        if(Object != null)
        {
            Object.GetComponent<Collider>().enabled=true;

            holdingObject = false;

            PlayerTransform.DetachChildren();

            Object.GetComponent<Rigidbody>().useGravity = true;
            Object.GetComponent<Rigidbody>().drag = 0f;
            
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    void Throw()
    {
        if(holdingObject == true)
        {
            FindObjectOfType<audioManager>().Play("throw");

            Drop();
            holdingObject = true;
            Object.GetComponent<Rigidbody>().velocity = camTransform.forward * 15;
        }
    }
    
}