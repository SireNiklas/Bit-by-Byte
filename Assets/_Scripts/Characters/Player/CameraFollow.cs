using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : Singleton<CameraFollow>
{
    
    CinemachineFreeLook _cinemachineFreeLook;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log(
            "<color=lime><b>Camera Follow Script</b> | <i>Assets/Scripts/CameraFollow.cs</i> | Loaded and Initiated.</color>");
    
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void CameraAttach(Transform followTransform, Transform lookAtTransform)
    {
        
        Camera.main.GetComponent<CinemachineFreeLook>().enabled = true;
        _cinemachineFreeLook.Follow = followTransform;
        _cinemachineFreeLook.LookAt = lookAtTransform;
        
    }
    
    
}