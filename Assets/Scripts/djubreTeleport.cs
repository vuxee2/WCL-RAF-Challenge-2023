using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class djubreTeleport : MonoBehaviour
{
    private bool grounded;

    public LayerMask whatIsGround;

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 100f, whatIsGround);

        if(!grounded && !objectHold.holdingObject)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);
        }
    }
}
