using System;
using SirNiklas.Core.Singletons;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] TextMeshProUGUI playerCount;
    
    [SerializeField] private Button startServer;
    [SerializeField] private Button startHost;
    [SerializeField] private Button startClient;

    public GameObject MainUI;
    private void Awake()
    {
        Cursor.visible = true;
        
    }
    
    void Update()
    {
        playerCount.text = $"Players in game: {PlayersManager.Instance.PlayerCount}";
    }

    private void Start()
    {
        startHost.onClick.AddListener(() =>
        {

            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Server & Client has been started...");

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                MainUI.SetActive(false);

            }
            else
            {
                Debug.Log("Server & Client could not be started...");

            }
        });

        startClient.onClick.AddListener(() =>
        {
            
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client has been started...");

                
            }
            else
            {
                Debug.Log("Client could not be started...");

                
            }
        });

        startServer.onClick.AddListener(() =>
        {
            
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server has been started...");

                
            }
            else
            {
                Debug.Log("Server could not be started...");

                
            }
        });

    }
}
