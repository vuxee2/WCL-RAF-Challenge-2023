using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSkybox : MonoBehaviour
{
    public float RotateSpeed = .15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
