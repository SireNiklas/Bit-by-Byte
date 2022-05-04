using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : Singleton<CameraFollow>
{
    
    CinemachineVirtualCamera _cinemachineVirtualCamera;
    CinemachineFreeLook _cinemachineFreeLook;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log(
            "<color=lime><b>Camera Follow Script</b> | <i>Assets/Scripts/CameraFollow.cs</i> | Loaded and Initiated.</color>");
    
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void CameraFirstPerson(Transform followTransform)
    {
        
        Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
        _cinemachineVirtualCamera.Follow = followTransform;
        
    }
    
    public void CameraThirdPerson(Transform followTransform, Transform lookAtTransform)
    {
        
        Camera.main.GetComponent<CinemachineFreeLook>().enabled = true;
        _cinemachineFreeLook.Follow = followTransform;
        _cinemachineFreeLook.LookAt = lookAtTransform;

    }
    
    
}