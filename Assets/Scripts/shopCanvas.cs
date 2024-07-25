using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class shopCanvas : MonoBehaviour
{
    public TMP_Text e;
    public TMP_Text q; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moneyManagement.moneyValue < 150) e.color = new Color (255,0,0);
        else e.color = new Color (255,255,255);

        if(moneyManagement.moneyValue < 750) q.color = new Color (255,0,0);
        else q.color = new Color (255,255,255);
    }
}
