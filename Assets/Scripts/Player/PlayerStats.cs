using UnityEngine;

public class PlayerStats : MonoBehaviour
{

/*    PlayerMovementController _playermovementcontroller;
    public int playerstamina;
    [SerializeField] private float regenrate = 0.01f;
    
    private PlayerHUD _hudController;

    private void Awake()
    {
        _playermovementcontroller = GetComponent<PlayerMovementController>();
        _hudController = GameObject.FindWithTag("PlayerHud").GetComponent<PlayerHUD>();
        
        InvokeRepeating("Regenerate", 0.0f, regenrate);

    }

    // Player Check passed, and stamina now being modifed.

    public void Regenerate()
    { 
        if (_playermovementcontroller.playerstamina < 1000) 
            _playermovementcontroller.playerstamina++;

        playerstamina = _playermovementcontroller.playerstamina;
        _hudController.PlayerStatsUpdater();
    }
    
    public void PlayerStaminaHandler()
    {
        _playermovementcontroller.playerstamina -= 1;

        playerstamina = _playermovementcontroller.playerstamina;

        _hudController.PlayerStatsUpdater();

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