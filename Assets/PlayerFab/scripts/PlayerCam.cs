using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    [Header("Cam Sensitivity")]
    public float sensX;
    public float sensY;
    public Transform orientation;
    float xRotation;
    float yRotation;

    [Header("Interaction")]
    bool active = false;
    bool interactActive = false;
    public float interactDistance;
    public KeyCode interactKey = KeyCode.Mouse0;
    public bool interactReady;
    public Image playerCursor;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interactReady = true;
        playerCursor.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        CamRotation();
        RayCastInteract();
    }
    public void CamRotation()
    {
        //mouse input for rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    public void RayCastInteract()
    {
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);
        RaycastHit hit;
        active = Physics.Raycast(transform.position, transform.forward, out hit, interactDistance);
        if (active)
        {
            if (hit.transform.gameObject.tag == "interactable")
            {
                interactActive = true;
                playerCursor.color = Color.red;
            }
            else
                playerCursor.color = Color.black;
        }
        else
            playerCursor.color = Color.black;

        if (Input.GetKey(interactKey) && interactReady && interactActive)
        {
            interactReady = false;
            Interact(hit);
        }
        if (Input.GetKeyUp(interactKey))
            interactReady = true;
    }
    public void Interact(RaycastHit hit)
    {
        if (active)
        {
            Debug.Log(hit.transform.gameObject.name);
                hit.transform.gameObject.GetComponent<Interactable>().OnInteracted();
        }

    }
}

