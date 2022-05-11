using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class HUDController : MonoBehaviour
{
    CinemachineFreeLook _cinemachineFreeLook;
    
    public Slider healthBar;
    public Slider staminaBar;
    public Slider Sense;

    //PlayerMovementController _playermovementcontroller;
    private PlayerStats _playerStats;

    private int _playerStamina;
    //private bool _isRunning;

    private Transform _lookat;
    private Transform _localtransfrom;

    private void Awake()
    {

        Debug.Log("<color=lime><b>HUD Controller Script</b> | <i>Assets/Scripts/Manager/HUDManager.cs</i> | Loaded and Initiated.</color>");
        //_playermovementcontroller = GetComponent<PlayerMovementController>();
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        _localtransfrom = GetComponent<Transform>();

        _lookat = Camera.main.transform;

    }
    
    void Update()
    {

        if (_lookat)
        {
            
            _localtransfrom.LookAt(2 * _localtransfrom.position - _lookat.position);
            
        }
        
        //Sense.value = _cinemachineFreeLook.m_XAxis.m_MaxSpeed;
    }

    // Player stam modifed and now displayed.
    
    public void PlayerStatsUpdater()
    {
        staminaBar.value = _playerStats.playerstamina;
    }
}
