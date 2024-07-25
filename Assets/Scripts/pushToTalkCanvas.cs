using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushToTalkCanvas : MonoBehaviour
{
    private KeyCode pushToTalk_KC;
    public GameObject mic_on;
    public GameObject mic_off;
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
            mic_on.SetActive(true);
            mic_off.SetActive(false);
        }
        else if(Input.GetKeyUp(pushToTalk_KC))
        {
            mic_on.SetActive(false);
            mic_off.SetActive(true);
        }
    }
}
