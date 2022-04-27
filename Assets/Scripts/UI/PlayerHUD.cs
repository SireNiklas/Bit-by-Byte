using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

// On Player, Add to player hud.

public class PlayerHUD : NetworkBehaviour
{
    /*public Slider healthBar;
    public Slider staminaBar;

    //PlayerMovementController _playermovementcontroller;
    private PlayerStats _playerStats;

    private int _playerStamina;
    //private bool _isRunning;

    private Transform _lookat;
    private Transform _localtransfrom;*/

    private NetworkVariable<NetworkStrings.NetworkString> playersName = new NetworkVariable<NetworkStrings.NetworkString>();

    private bool overlaySet = false;

    public override void OnNetworkSpawn()
    {

        if (IsServer)
        {
            playersName.Value = $"Player {OwnerClientId}";

        }
    }

    public void SetOverlay()
    {

        var localPlayerOverlay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        localPlayerOverlay.text = playersName.Value;

    }
/*    private void Awake()
    {

        Debug.Log("<color=lime><b>HUD Controller Script</b> | <i>Assets/Scripts/Manager/HUDManager.cs</i> | Loaded and Initiated.</color>");
        //_playermovementcontroller = GetComponent<PlayerMovementController>();
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        _localtransfrom = GetComponent<Transform>();

        _lookat = Camera.main.transform;

    }*/

    void Update()
    {

        if (!overlaySet && !string.IsNullOrEmpty(playersName.Value))
        {
            SetOverlay();
            overlaySet = true;

        }
    }

    /*if (_lookat)
    {
        
        _localtransfrom.LookAt(2 * _localtransfrom.position - _lookat.position);
        
    }
}

// Player stam modifed and now displayed.

public void PlayerStatsUpdater()
{ 
    staminaBar.value = _playerStats.playerstamina;
}*/
}
