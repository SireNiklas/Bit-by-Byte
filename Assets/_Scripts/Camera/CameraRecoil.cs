using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{

    public static CameraRecoil Instance { get; private set; }

    
    private CinemachineFreeLook _cinemachineFreeLook;
    private float _shakeTimer;

    private void Awake()
    {
        Instance = this;
        
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void Recoil(float intensity, float time)
    {
        
        CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = _cinemachineFreeLook.GetComponent<CinemachineBasicMultiChannelPerlin>();

        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _shakeTimer = time;

    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {

            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                // Time over!
                
                CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin = _cinemachineFreeLook.GetComponent<CinemachineBasicMultiChannelPerlin>();

                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }

        }
    }
}
