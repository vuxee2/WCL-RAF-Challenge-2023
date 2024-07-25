using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class moneyManagement : MonoBehaviourPun
{
    public static int moneyValue;
    public TMP_Text moneyTXT;

    // Start is called before the first frame update
    void Start()
    {
        moneyValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyTXT.text = moneyValue.ToString();
    }
    
}
