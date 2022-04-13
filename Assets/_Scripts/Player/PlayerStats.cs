using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    PlayerMovementController _playermovementcontroller;
    public int playerStaminaStat;
    [SerializeField] float regenRate = 0.1f;
    
    private HUDController _hudController;

    private void Awake()
    {
        _playermovementcontroller = GetComponent<PlayerMovementController>();
        _hudController = GameObject.Find("ThirdPHud").GetComponent<HUDController>();
        
        InvokeRepeating("Regenerate", 0.0f, regenRate);

    }

    // Player Check passed, and stamina now being modifed.
    
    
    public void Regenerate()
    { 
        if (_playermovementcontroller.playerStamina < 1000) 
            _playermovementcontroller.playerStamina++;

        playerStaminaStat = _playermovementcontroller.playerStamina;
        _hudController.PlayerStatUpdater();
    }
    
    public void PlayerStaminaHandler()
    {
        
            Debug.Log("Running in Stats Handler");
            _playermovementcontroller.playerStamina -= 1;

            playerStaminaStat = _playermovementcontroller.playerStamina;

            _hudController.PlayerStatUpdater();

    }


/*float fallTime = 0;
bool hasFallen = false;

void FallDamage()
{
    
    if (!_charController.isGrounded)
    {
        Debug.Log(_charController.velocity);
    }
    
    if (_charController.velocity.y < 0)
    {
        fallTime += Time.deltaTime;

        // The player "has fallen" I guess
        hasFallen = true;
        
    } else if (hasFallen) {
        Debug.Log(fallTime);

        // If the player has fallen a considerable amount of time
        if (fallTime > 1) {
            // Hurt the player based on how long they fell
            playerHealth -= (int)fallTime * 5;
        }
        hasFallen = false;
        fallTime = 0;
    }*/
}