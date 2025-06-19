using System.Collections;
using TMPro;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    Transform camTransform;

    void Start()
    {
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        var camForward = camTransform.forward;

        if (camForward.sqrMagnitude > 0.001f)
        {
            var upwardTilt = Quaternion.Euler(0f, 0f, 0f); // Adjust tilt angle here
            transform.rotation = Quaternion.LookRotation(camForward) * upwardTilt;
        }
    }
}