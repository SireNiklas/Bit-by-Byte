using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    
    private CharacterController _charController;
    private PlayerControls playerControls;

    PlayerStats _playerStats;
    
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpHeight;
    
    public int playerStamina = 1000;
    
    private Vector3 _playerMovement;
    private Vector3 lastPos;
    
    [SerializeField] public bool isRunning;

    private Transform cameraTransform;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        _charController = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();
        //_playerStats = GetComponent<PlayerStatsHandler>().PlayerStaminaHandler();
        
        cameraTransform = Camera.main.transform;

        playerControls = new PlayerControls();
        playerControls.Movement.Enable();
        playerControls.Movement.Jump.performed += MovementJump;
        playerControls.Movement.WASD.performed += MovementWASD;
        playerControls.Movement.Sprint.started += MovementSprint;
        playerControls.Movement.Sprint.canceled += MovementSprint;
        
    }

    private void MovementWASD(InputAction.CallbackContext context)
    {

    }

    private void MovementSprint(InputAction.CallbackContext context)
    {
        if (!isRunning && context.started && playerStamina > 0)
        {
            playerSpeed = playerSpeed * 2f;
            isRunning = true;
            
        } else if (isRunning && context.canceled)
        {

            playerSpeed = 10f;
            isRunning = false;
            
        }

    }

    private void SprintHandler()
    {
        
        if (isRunning)
        {
            
            _playerStats.PlayerStaminaHandler();

        }
        
        if (isRunning && isRunning && playerStamina < 1)
        {
            
            playerSpeed = 10f;
            isRunning = false;
            
        }

    }

    private void MovementJump(InputAction.CallbackContext context)
    {
        if (_charController.isGrounded)
        {

            _playerMovement.y = playerJumpHeight;
        }
    }
    
    private void Update()
    {
        
        PlayerMovement();
        SprintHandler();

    }
    
    private void PlayerMovement()
    {
        
        /*
         * Fix Player speed dictated by the Camera angle.
         * Either Rotate player on A and D press, or animate a strafe.
         */
        
        // WASD Movement section.
        Vector2 inputVector = playerControls.Movement.WASD.ReadValue<Vector2>();
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        // Rename WASD_movement - to something.
        
        // Move camera relative to Player.
        WASD_movement = WASD_movement.z * cameraTransform.forward.normalized + WASD_movement.x * transform.right.normalized;
        WASD_movement.y = 0f;

        _charController.Move(WASD_movement * Time.deltaTime * playerSpeed);
        
        //Debug.Log("<color=cyan>Mouse Position: <b>" + Input.mousePosition + "</b></color>");
        
        _playerMovement.y -= 9.81f * Time.deltaTime;
        _charController.Move(_playerMovement * Time.deltaTime);
        // Debug.Log("<color=cyan> " + transform.position + " </color>");
        
        //Debug.Log(playerControls.Movement.WASD.ReadValue<Vector2>().normalized);
        //Debug.Log(WASD_movement);   

    }
    
}