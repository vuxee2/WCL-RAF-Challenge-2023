using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreControl : MonoBehaviour
{
    public static int treeHealth = 25;
    public static int treeHealth_ = 25;
    private GameObject[] trashes;

    public GameObject tree;
    Rigidbody treeRB;
    public GameObject eventCam;
    public GameObject fadeIn;
    private bool isEnded = false;

    public GameObject moneyCanvas;
    public GameObject EndScreenCanvas;

    public Animator musicAnim;
    public Animator musicAnim2;
    // Start is called before the first frame update
    void Start()
    {
        treeRB = tree.GetComponent<Rigidbody>();

        moneyCanvas.SetActive(true);
        EndScreenCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        trashes = GameObject.FindGameObjectsWithTag("Object");
        
        treeHealth = treeHealth_;
        foreach(GameObject t in trashes)
        {
            treeHealth--;
        }
        
        if(treeHealth == 0 && !isEnded)
        {
            isEnded = true;

            spawnObjects.stopSpawning = true;

            fadeIn.SetActive(true);

            Time.timeScale = 0.25f;
            StartCoroutine(WaitForEnd());
        }
        
    }


    IEnumerator WaitForEnd()
    {
        musicAnim.SetTrigger("fadeOut");
        musicAnim2.SetTrigger("fadeIn");

        yield return new WaitForSeconds(2);

        FindObjectOfType<audioManager>().Play("treeFall");
        
        Time.timeScale = 1f;
        fadeIn.SetActive(false);
        eventCam.SetActive(true);

        treeRB.constraints = RigidbodyConstraints.None;

        moneyCanvas.SetActive(false);
        EndScreenCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
