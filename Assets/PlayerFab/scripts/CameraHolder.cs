using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform cameraPosition;
    // Update is called once per frame

    public float bobAmplitude;
    public float bobFrequency;
    public Rigidbody rb;

    private void Start()
    {
        
    }
    void Update()
    {
        transform.position = cameraPosition.position;
        checkMotion();
    }
    void checkMotion()
    {
        
        if (rb.velocity.x > 0 || rb.velocity.y > 0)
        {
            FootStepMotion();
        }
    }
    void playMotion(Vector3 motion)
    {

    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        pos.y += Mathf.Sin(Time.time * bobFrequency / 2) * bobAmplitude / 2;
        transform.localPosition += pos;
        return pos;
    }
    //private void ResetBob() maybe not neccesary with the update looking the way it is
    //{
    //    if (transform.localPosition == StartPos) return;
    //    transform.localPosition =
    //}
}
