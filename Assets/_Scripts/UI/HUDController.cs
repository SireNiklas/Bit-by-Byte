using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    public GameObject Player;
    public TextMeshProUGUI debugLog;
    public TextMeshProUGUI healthStat;
    public TextMeshProUGUI staminaStat;

    public Slider healthBar;
    public Slider staminaBar;

    private void Awake()
    {

        Debug.Log("<color=lime><b>HUD Manager Script</b> | <i>Assets/Scripts/Manager/HUDManager.cs</i> | Loaded and Initiated.</color>");

    }

        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        debugLog.text = Player.transform.position.ToString();
        healthStat.text = PlayerController.playerHealth.ToString();
        healthBar.value = PlayerController.playerHealth;
        staminaStat.text = PlayerController.playerStamina.ToString();
        staminaBar.value = PlayerController.playerStamina;
    }
}
