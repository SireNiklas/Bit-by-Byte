using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    public GameObject Player;
    public TextMeshProUGUI debugLog;
    
    public Slider healthBar;
    public Slider staminaBar;

    PlayerMovementController _playermovementcontroller;
    private PlayerStats _playerStats;

    private int _playerStamina;
    //private bool _isRunning;

    public Transform mLookAt;
    private Transform localTransform;

    private void Awake()
    {

        Debug.Log("<color=lime><b>HUD Controller Script</b> | <i>Assets/Scripts/Manager/HUDManager.cs</i> | Loaded and Initiated.</color>");
        _playermovementcontroller = GetComponent<PlayerMovementController>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        localTransform = GetComponent<Transform>();

    }
    
    void Update()
    {

        if (mLookAt)
        {
            
            localTransform.LookAt(2 * localTransform.position - mLookAt.position);
            
        }

        //debugLog.text = Player.transform.position.ToString();
        //healthStat.text = PlayerMovementController.playerHealth.ToString();
        //healthBar.value = PlayerMovementController.playerHealth;
        //staminaStat.text = PlayerMovementController.playerStamina.ToString();
    }

    // Player stam modifed and now displayed.
    
    public void PlayerStatUpdater()
    { 
        staminaBar.value = _playerStats.playerStaminaStat;
    }
}
