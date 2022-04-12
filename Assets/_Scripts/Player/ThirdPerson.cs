using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdPerson : MonoBehaviour
{

    [SerializeField] private float playerRotation;

    private Vector3 lastPos;

    private Transform cameraTransform;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Third Person Controller Script</b> | <i>Assets/Scripts/Player/ThirdPersonController.cs</i> | Loaded and Initiated.</color>");
        
        cameraTransform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerRotation();
        
    }
    private void PlayerRotation()
    { 
        // Rotate camera relative to Player.
        // Make ThirdCamMaster get World Transform

        if (lastPos.x != transform.position.x && lastPos.z != transform.position.z)
        {
            // Add Character (look) Rotation dictated by WASD keypress.
            
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerRotation * Time.deltaTime);
        }

        lastPos.x = transform.position.x;
        lastPos.z = transform.position.z;

        //transform.rotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        //Transform.rotation = Quaternion.LookRotation(WASD_movement);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(gameObject.transform.forward), 0.15F);

        //Debug.Log("<color=cyan>Target Rotation</color>: <color=cyan>" + targetRotation + "</color>");

        // WASD Rotates character.- TO DO.

    }


    /*private void CameraFocal(InputAction.CallbackContext context)
    {

        float focalview = playerControls.Camera.FocalView.ReadValue<float>();
        if (focalview > 0)
        {
            
            Debug.Log("Scroll up");
            // add Zoom

        } else if (focalview < 0)
        {
            
            Debug.Log("Scroll down");
            // add Zoom

        }

    }*/

}
