using SirNiklas.Core.Singletons;
using UnityEngine;
using Cinemachine;

public class CameraFollow : Singleton<CameraFollow>
{
    
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineFreeLook cinemachineFreeLook;
    
    private void Awake()
    {
    }

    public void Start()
    {
        
        Debug.Log(
            "<color=lime><b>Third Person Controller Script</b> | <i>Assets/Scripts/Player/ThirdPersonController.cs</i> | Loaded and Initiated.</color>");

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        
    }

    public void FirstPersonFollow(Transform transform)
    {
        
        cinemachineVirtualCamera.Follow = transform;

    }
    
    public void ThirdPersonFollow(Transform transform)
    {
        
        cinemachineFreeLook.Follow = transform;

    }
    
    public void ThirdPersonLookAt(Transform transform)
    {
        
        cinemachineFreeLook.LookAt = transform;

    }

}
