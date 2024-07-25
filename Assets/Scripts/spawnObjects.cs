using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnObjects : MonoBehaviour
{
    public GameObject Object;
    public static bool stopSpawning = true;
    private int[] spawnDelay = {10, 5, 2, 1};
    private int i = 0;

    private bool spawning;

    public static float spawnRate;
    
    private Vector3[] randomPos = new Vector3[3];

    void Start()
    {
        stopSpawning = true;
        spawning = false;
    }
    void Update()
    {
        if(!stopSpawning && !spawning)
        {
            spawning = true;
            StartCoroutine(changeIndex(20));
            StartCoroutine(Spawn(spawnDelay[i%4]));
        }
            
    }

    public void doSpawn()
    {
        randomPos[0] = new Vector3(Random.Range(80,165), -10, Random.Range(70,135));
        randomPos[1] = new Vector3(Random.Range(170,240), -10, Random.Range(135,260));
        randomPos[2] = new Vector3(Random.Range(60,165), -10, Random.Range(135,270));
        
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(Object.name,randomPos[Random.Range(0,3)],Quaternion.identity);
    }

    IEnumerator Spawn(int delay)
    {
        yield return new WaitForSeconds(delay);
        doSpawn();
        spawnRate = 1 / (float)spawnDelay[i%4];

        if(!stopSpawning)
            StartCoroutine(Spawn(spawnDelay[i%4]));
    }
    IEnumerator changeIndex(int delay)
    {
        playerUI.timer = delay;

        yield return new WaitForSeconds(delay);
        i++;

        int delay2 = 30;

        if(i%4 == 0) delay2 = 30;
        if(i%4 == 1) delay2 = 60;
        if(i%4 == 2) delay2 = 15;
        if(i%4 == 3) delay2 = 5;

        StartCoroutine(changeIndex(delay2));
    }
}
