using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject player;
    public UnityEvent interactAction;
    public GameObject waypoint;
    public bool temporary;
    public void OnInteracted()
    {
        if (!temporary)
        GameManager.Instance.OnPlayerTeleport(waypoint);

        if (temporary)
        {
            GameManager.Instance.OnPlayerTeleport(waypoint);
            GameManager.Instance.memories++;
            this.gameObject.SetActive(false);
        }
        
    }

}
