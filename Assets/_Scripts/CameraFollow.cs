using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : Singleton<CameraFollow>
{
    
    CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineFreeLook _cinemachineFreeLook;

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
        
        _cinemachineVirtualCamera.Follow = followTransform;
        
    }
    
    public void CameraThirdPerson(Transform followTransform, Transform lookAtTransform)
    {

        if (!_cinemachineFreeLook) return;
        
        _cinemachineFreeLook.enabled = true;
        _cinemachineFreeLook.Follow = followTransform;
        _cinemachineFreeLook.LookAt = lookAtTransform;

    }
    
    
}