using System;
using SirNiklas.Core.Singletons;
using Unity.Netcode;
//using UnityEditor.PackageManager;
using UnityEngine;

public class PlayersManager : NetworkSingleton<PlayersManager>
{

    public NetworkVariable<int> playerCount = new NetworkVariable<int>();
    
    private NetworkPlayerMovementController _networkplayermovementcontroller;
    
    private GameObject player;

    private Transform gameobject;

    private GameObject cam;

    private float playerid;

    private PlayerCheck _playerCheck;
    
    //public GameObject cameralooktarget;
    
    public int PlayerCount
    {
        
        get
        {
            return playerCount.Value;
        }
        
    }
    
    /*public override void OnNetworkSpawn()
    {
        Debug.Log("Client connected" + " Is Client:" + IsClient + "\nIs Host:" + IsHost);
        if (IsClient)
        {

            //GetComponent<NetworkPlayerMovementController>().enabled = true;

            NetworkLog.LogInfoServer("Client connected stuff should turn on!");

            //GetComponent<NetworkPlayerMovementController>().enabled = true;
            
            //GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkPlayerMovementController>().enabled = true;

            //player = GameObject.FindWithTag("Player");

            //_playerCheck.EnablePlayerController();

            //_networkplayermovementcontroller.enabled = true;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPerson>().enabled = true;
            //Camera cam = GameObject.Find("MainCamera").GetComponent<Camera>();

            //cam.enabled = true;

            //Transform _networkplayermovementcontroller._cameratransform = Camera.main.gameObject.transform;
            //_networkplayermovementcontroller._cameratransform.parent = gameobject.Find("CameraLookTarget");
            //_networkplayermovementcontroller._cameratransform = player.gameObject.transform;

            //player = GameObject.FindWithTag("Player");

            Debug.Log("Player has been enabled.");

        }
    }*/

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {

            //Debug.Log("Client connected" + " Is Client:" + IsClient + "\nIs Host:" + IsHost);
            if (IsServer)
            {
                Debug.Log($"{id} Just connected...");
                playerCount.Value++;
            }

            if (IsClient)
            {
            }

        };
        
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                Debug.Log($"{id} Just disconnected...");
                playerCount.Value--;
            }

        };

    }
}
