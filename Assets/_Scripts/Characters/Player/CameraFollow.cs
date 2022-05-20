using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    
    public static CameraFollow Instance { get; private set; }

    CinemachineFreeLook _cinemachineFreeLook;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        
    }

    private void OnEnable()
    {
        Debug.Log(
            "<color=lime><b>Camera Follow Script</b> | <i>Assets/Scripts/CameraFollow.cs</i> | Loaded and Initiated.</color>");
    
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        
        DontDestroyOnLoad(gameObject);
    }

    public void CameraAttach(Transform followTransform, Transform lookAtTransform)
    {
        
        Camera.main.GetComponent<CinemachineFreeLook>().enabled = true;
        _cinemachineFreeLook.Follow = followTransform;
        _cinemachineFreeLook.LookAt = lookAtTransform;
        
    }


}