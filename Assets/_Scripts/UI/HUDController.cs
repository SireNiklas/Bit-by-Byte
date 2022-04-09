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

    public Transform mLookAt;
    private Transform localTransform;

    private void Awake()
    {

        Debug.Log("<color=lime><b>HUD Controller Script</b> | <i>Assets/Scripts/Manager/HUDManager.cs</i> | Loaded and Initiated.</color>");

    }

        // Start is called before the first frame update
    void Start()
    {

        localTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        if (mLookAt)
        {
            
            localTransform.LookAt(2 * localTransform.position - mLookAt.position);
            
        }

        debugLog.text = Player.transform.position.ToString();
        healthStat.text = ThirdPersonController.playerHealth.ToString();
        healthBar.value = ThirdPersonController.playerHealth;
        staminaStat.text = ThirdPersonController.playerStamina.ToString();
        staminaBar.value = ThirdPersonController.playerStamina;

    }
}
