using UnityEngine;
using Cinemachine;

public class CameraFollow : Singleton<CameraFollow>
{

    CinemachineFreeLook _cinemachineFreeLook;

    private void OnEnable()
    {
#if UNITY_EDITOR
        Debug.Log(
            "<color=lime><b>Camera Follow Script</b> | <i>Assets/Scripts/CameraFollow.cs</i> | Loaded and Initiated.</color>");
#endif
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void CameraAttach(Transform followTransform, Transform lookAtTransform)
    {
        
        Camera.main.GetComponent<CinemachineFreeLook>().enabled = true;
        _cinemachineFreeLook.Follow = followTransform;
        _cinemachineFreeLook.LookAt = lookAtTransform;
        
    }


}