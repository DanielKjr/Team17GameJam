using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField]
    private bool bobEnabled = true;

    [SerializeField, Range(0, 0.1f)]
    private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)]
    private float frequency = 10f;

    [SerializeField]
    private Transform cameraPos = null;
    [SerializeField]
    private Transform cameraHolder = null;

    private float toggleSpeed = 3f;
    private Vector3 startPos;
    private CharacterController charController;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        startPos = cameraPos.position;
    }

}
