using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    PlayerMovementController _playerMovementController;
    private CharacterController _characterController;
    
    public int playerStamina = 1000;
    public float playerHealth = 100;
    
    [SerializeField] private float regenrate = 0.01f;
    
    private HUDController _hudController;

    [Header("Player Attributes")] 
    [SerializeField]
    private bool _wasGrounded;
    [SerializeField]
    private bool _grounded;

    private RaycastHit _fallHit;
    
    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _characterController = GetComponent<CharacterController>();
        _hudController = GameObject.FindWithTag("PlayerHud").GetComponent<HUDController>();
        
        InvokeRepeating("Regenerate", 0.0f, regenrate);

    }

    private float damage;

    /*private void FixedUpdate()
    {
        damage = Mathf.Abs(_characterController.velocity.y) + _velocityThreshold;

        if (_characterController.isGrounded)
        {
            if (_characterController.velocity.y < -_velocityThreshold)
            {
                
                Debug.Log("DO DAMAGE: " + damage);

                playerHealth -= damage * 2;
                
            }

        }
        
        Debug.Log("Velocity: " + damage);

    }*/

    [SerializeField] private float _groundedPosition;
    [SerializeField] private float _fallDistance;
    [SerializeField] private float _minimumFallDistance = 5f;

    private void Update()
    {

        if (_characterController.isGrounded)
        {

            //Debug.Log("GROUNDED!");

            _groundedPosition = transform.position.y;

            if (_fallDistance > _minimumFallDistance)
            {
                //playerHealth -= _fallDistance;
                
                _fallDistance = 0;
            }
        }
        else if (!_characterController.isGrounded) 
        { 
            if (Physics.Raycast(transform.position, Vector3.down, out _fallHit, 999f))
            {
                _fallDistance = _groundedPosition - _fallHit.point.y;
                Debug.DrawLine(transform.position, _fallHit.point, Color.green);
            }
        }

    }


    // Player Check passed, and stamina now being modifed.

    // Health & Stamaina Regeneration.
    public void Regenerate()
    { 
        if (playerStamina < 1000) 
            playerStamina++;

        if (playerHealth < 100) playerHealth += 0.01f;

        _hudController.PlayerStatsUpdater();
    }
    
    public void PlayerStaminaHandler()
    {
        playerStamina -= 1;

        _hudController.PlayerStatsUpdater();

    }

    private void DoVoidDamage()
    {

        playerHealth -= playerHealth * 0.7f;

    }

/*float fallTime = 0;
bool hasFallen = false;

void FallDamage()
{
    
    if (!_controller.isGrounded)
    {
        Debug.Log(_controller.velocity);
    }
    
    if (_controller.velocity.y < 0)
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