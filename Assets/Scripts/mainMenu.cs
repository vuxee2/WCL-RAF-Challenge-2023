using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Audio;

public class mainMenu : MonoBehaviour
{
    public Animator nameAnim;
    public Animator buttonsAnim;
    public Animator SPorMPAnim;
    
    public GameObject buttons;
    private bool canButtonsBeShown;

    public GameObject pressAnyKey;

    public GameObject SPorMP;

    public GameObject Likovi;

    public GameObject fadeIn;
    
    public Camera cam;
    private Rigidbody camRb;
    private int rotateDirection;

    public GameObject videoPlayer;
    private bool videoOver = false;
    [SerializeField]
    VideoPlayer myVideoPlayer;

    public GameObject mainMenuCanvas;
    public GameObject OptionsGO;
    public GameObject WCLGO;
    public GameObject lobbyGO;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        SetSettings();

        myVideoPlayer.loopPointReached += disableVideo;

        //Time.timeScale = 0;

        camRb = cam.GetComponent<Rigidbody>();
        
        videoPlayer.SetActive(true);
        mainMenuCanvas.SetActive(false);
        WCLGO.SetActive(false);

        buttons.SetActive(false);
        pressAnyKey.SetActive(false);
        SPorMP.SetActive(false);
        fadeIn.SetActive(false);
        Likovi.SetActive(false);

        canButtonsBeShown = false;

        StartCoroutine(activeMainMenu());
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey &&  canButtonsBeShown && videoOver)
        {
            nameAnim.SetFloat("Speed",1.0f);
            pressAnyKey.SetActive(false);
            
            StartCoroutine(WaitForButtons());

            canButtonsBeShown = false;
        }
        if(Input.GetKeyDown(KeyCode.Space)  && !videoOver)
        {
            videoPlayer.SetActive(false);
            videoOver = true;
            //Time.timeScale = 1f;
            mainMenuCanvas.SetActive(true);
            WCLGO.SetActive(true);
            StartCoroutine(WaitForName());
        }
    }

    public void disableVideo(VideoPlayer vp)
    {
        videoPlayer.SetActive(false);
        videoOver = true;
        //Time.timeScale = 1f;
    }

    IEnumerator WaitForName()
    {
        yield return new WaitForSeconds(4);
        
        nameAnim.SetFloat("Speed",0.0f);
        pressAnyKey.SetActive(true);
        canButtonsBeShown = true;
    }
    IEnumerator WaitForButtons()
    {
        yield return new WaitForSeconds(2);
        buttons.SetActive(!buttons.activeSelf);
        camRb.angularDrag = 10f;
    }
    IEnumerator WaitForSPorMP()
    {
        yield return new WaitForSeconds(2);
        SPorMP.SetActive(!SPorMP.activeSelf);
        lobbyGO.SetActive(SPorMP.activeSelf);
        camRb.angularDrag = 10f;
    }
    IEnumerator WaitForLikovi()
    {
        yield return new WaitForSeconds(1);
        Likovi.SetActive(!Likovi.activeSelf);
    }
    IEnumerator WaitForExit()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
    IEnumerator activeMainMenu()
    {
        yield return new WaitForSeconds(105);

        if(!videoOver)
        {
            mainMenuCanvas.SetActive(true);
            WCLGO.SetActive(true);
            StartCoroutine(WaitForName());
            videoOver = true;
        }
    }

    private void RotateCamera360()
    {
        camRb.angularDrag = 0.97f;
        camRb.AddTorque(rotateDirection * cam.transform.right * 1000);
    }


    public void PlayButton()
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        rotateDirection = -1;
        RotateCamera360();

        SPorMPAnim.SetBool("Back",false);

        StartCoroutine(WaitForButtons());
        StartCoroutine(WaitForSPorMP());

        StartCoroutine(WaitForLikovi());
        
        buttonsAnim.SetBool("Back",true);
        
    }
    public void BackButton()
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        rotateDirection = 1;
        RotateCamera360();

        buttonsAnim.SetBool("Back",false);

        StartCoroutine(WaitForButtons());
        StartCoroutine(WaitForSPorMP());

        StartCoroutine(WaitForLikovi());

        SPorMPAnim.SetBool("Back",true);

    }
    public void ExitButton()
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        StartCoroutine(WaitForExit());
        fadeIn.SetActive(true);
    }
    public void OptionsButton()
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        OptionsGO.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void SetSettings()
    {
        audioMixer.SetFloat("music",PlayerPrefs.GetFloat("musicVolume"));
        audioMixer.SetFloat("sfx",PlayerPrefs.GetFloat("sfxVolume"));
    }
}
