using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameObject player;
    public GameObject cameraHolder;
    public Image blackScreen;
    public Animator anim;
    public GameObject exitVictory;
    public int memories;
    public VideoPlayer vidplayer;
    public GameObject vidPlayerObject;
    public GameObject EndScreen;

    private void Awake()
    {
        memories = 0;
    }
    public void Update()
    {
        if (memories == 2)
        {
            exitVictory.SetActive(true);
        }
        if (vidplayer.clockTime > 6)
        {
            vidPlayerObject.SetActive(false);
        }
    }
    
    public void OnPlayerTeleport(GameObject waypoint)
    {
        StartCoroutine(fadeinoutmove(waypoint));
    }
    IEnumerator fadeinoutmove(GameObject waypoint)
    {
        anim.SetBool("Fade", true);
        player.SetActive(false);
        yield return new WaitUntil(() => blackScreen.color.a == 1);
        player.transform.position = waypoint.transform.position;
        anim.SetBool("Fade", false);
        player.SetActive(true);
        
    }
    public void Victory()
    {
        Debug.Log("you did it");
        EndScreen.SetActive(true);
    }


    
}
