using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject player;
    public UnityEvent interactAction;
    public GameObject waypoint;
    public void onInteracted()
    {
        Debug.Log("i done been interacted wit'");
        GameManager.Instance.OnPlayerTeleport(waypoint);
    }
 
}
