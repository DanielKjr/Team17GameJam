using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent interactAction;

    public void onInteracted()
    {
        Debug.Log("i done been interacted wit'");
    }
 
}
