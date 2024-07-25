using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
public class pushToTalk : MonoBehaviour
{
    public Recorder myRecorder;
    private KeyCode pushToTalk_KC;
    // Start is called before the first frame update
    void Start()
    {
        pushToTalk_KC = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pushToTalkKey"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pushToTalk_KC))
        {
            myRecorder.TransmitEnabled=true;
        }
        else if(Input.GetKeyUp(pushToTalk_KC))
        {
            myRecorder.TransmitEnabled=false;
        }
    }
}
