using System.Collections;
using System.Collections.Generic;
using SirNiklas.Core.Singletons;
using UnityEngine;
using Unity.Netcode;

public class PlayerMananger : NetworkSingleton<PlayerMananger>
{
    
    PlayerCheck _playerCheck;
    
    public override void OnNetworkSpawn()
    {
        Debug.Log("Client connected" + " Is Client:" + IsClient + "\nIs Host:" + IsHost);
        if (IsClient)
        {

            //GetComponent<NetworkPlayerMovementController>().enabled = true;

            //NetworkLog.LogInfoServer("Client connected stuff should turn on!");
            //GameObject.FindGameObjectWithTag("Player").
            //_playerCheck.EnablePlayerController();
            
            //NetworkSingleton
            
            //GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkPlayerMovementController>().enabled = true;

            //player = GameObject.FindWithTag("Player");

            //_networkplayermovementcontroller.enabled = true;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkPlayerMovementController>().enabled = true;
            //Camera cam = GameObject.Find("MainCamera").GetComponent<Camera>();

            //cam.enabled = true;

            //Transform _networkplayermovementcontroller._cameratransform = Camera.main.gameObject.transform;
            //_networkplayermovementcontroller._cameratransform.parent = gameobject.Find("CameraLookTarget");
            //_networkplayermovementcontroller._cameratransform = player.gameObject.transform;

            //player = GameObject.FindWithTag("Player");

            Debug.Log("Player has been enabled.");

        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
