using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    private Rigidbody _Rigidbody;
    
    private void Awake()
    {

        _Rigidbody = GetComponent<Rigidbody>();

    }

    void Start()
    {
        
        _Rigidbody.AddForce(Camera.main.transform.forward * 15f, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
