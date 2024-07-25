using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public static bool isDancing = false;
    public static bool isEscCanvasAcive = false;

    public static bool updateSensitivity = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(updateSensitivity)
        {
            sensX = PlayerPrefs.GetFloat("sens", 100);
            sensY = PlayerPrefs.GetFloat("sens", 100);
            updateSensitivity = false;
        }

        if(!isDancing && !isEscCanvasAcive && !playerUI.isTutOpen)
        {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        if(objectHold.holdingObject == false)
            xRotation = Mathf.Clamp(xRotation, -90f, 85f);
        else
            xRotation = Mathf.Clamp(xRotation, -90f, 50f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

    }
}
