using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Photon.Pun;

public class postProcessingControl : MonoBehaviour
{
    //public Volume volume;
    public Volume volume2;

    //UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    //UnityEngine.Rendering.Universal.Vignette vignette;
    //UnityEngine.Rendering.Universal.DepthOfField dof;
    //PhotonView view;
    
    //bool isHit = false;
    float hitDistance;

    // Start is called before the first frame update
    void Start()
    {
        //view = GetComponent<PhotonView>();

        // volume.profile.TryGet<UnityEngine.Rendering.Universal.ColorAdjustments>(out colorAdjustments);
        // volume.profile.TryGet<UnityEngine.Rendering.Universal.Vignette>(out vignette);
        //volume.profile.TryGet<UnityEngine.Rendering.Universal.DepthOfField>(out dof);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        volume2.weight = playerMovement.moveSpeed / 25;
        //DistanceWhereImLooking();
        //SetFocus();
    }   
    void Update()
    {
        /*if(PhotonNetwork.IsMasterClient)
        {
            colorAdjustments.saturation.value = scoreControl.treeHealth - (25-scoreControl.treeHealth)*2;
            vignette.smoothness.value = (float)(50-scoreControl.treeHealth) / 100; 
        }*/
            
    }
    /*void DistanceWhereImLooking()
    {
        var ray = new Ray(transform.position,transform.forward * 100);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,100f))
        {
            //isHit = true;
            hitDistance = Vector3.Distance(transform.position, hit.point);
        }
        else if(hitDistance < 100f)
        {
            hitDistance++;
        }
    }
    void SetFocus()
    {
        dof.focusDistance.value = hitDistance;
    }*/
}
