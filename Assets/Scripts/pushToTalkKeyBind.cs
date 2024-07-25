using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class pushToTalkKeyBind : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonTXT;

    // Start is called before the first frame update
    void Start()
    {
        buttonTXT.text = PlayerPrefs.GetString("pushToTalkKey");
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonTXT.text == "----")
        {
            foreach(KeyCode kc in Enum.GetValues(typeof(KeyCode)))
            {
                if(Input.GetKey(kc))
                {
                    buttonTXT.text = kc.ToString();
                    PlayerPrefs.SetString("pushToTalkKey",kc.ToString());
                    PlayerPrefs.Save();
                }
            }
        }
    }
    public void ChangeKey()
    {
        buttonTXT.text = "----";
    }
}
