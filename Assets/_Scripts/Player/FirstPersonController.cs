using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{


    private CharacterController _charController;

    private Vector3 _playerMovement;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpHeight;
    [SerializeField] private float playerRotation;

    [SerializeField] private bool isRunning;

    //[SerializeField] private float rotationSpeed = .8f;
    [SerializeField] public static int playerHealth = 100;
    [SerializeField] public static int playerStamina = 100;
    public float regenRate;
    
    [SerializeField] private GameObject firstPCam;
    [SerializeField] private GameObject firstPUI;

    private float playerStatDelay;

    private float GRAVITY = -9.81f;
    private Vector3 lastPos;

    private Transform cameraTransform;

    private PlayerControls playerControls;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Third Person Controller Script</b> | <i>Assets/Scripts/Player/ThirdPersonController.cs</i> | Loaded and Initiated.</color>");
        _charController = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;

        playerControls = new PlayerControls();
        playerControls.Movement.Enable();
        playerControls.Movement.Jump.performed += MovementJump;
        playerControls.Movement.WASD.performed += MovementWASD;
        playerControls.Movement.Sprint.started += MovementSprint;
        playerControls.Movement.Sprint.canceled += MovementSprint;
        
        InvokeRepeating("Regenerate", 0.0f, regenRate);
    }

    public void Start()
    {
        
    }
    
    void Regenerate()
    {
        
        if (playerStamina < 500)
            playerStamina++;
    }

    #region Movement Functions
    
    private void MovementWASD(InputAction.CallbackContext context)
    {

    }

    private void MovementSprint(InputAction.CallbackContext context)
    {
        if (!isRunning && context.started && playerStamina > 0)
        {
            if (gameObject.transform)
            {
                playerSpeed = playerSpeed * 2f;
                isRunning = true;
                playerStamina -= 1;
            }

        }

        if (isRunning && context.canceled)
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
    
    #endregion

    // Update is called once per frame
    void Update()
    {
        
        PlayerMovement();
        PlayerStats();
        FallDamage();
        
    }

    void LateUpdate()
    {
        //CharacterRotation();
    }

    #region Player Movement & Rotation

    private void PlayerRotation()
    { 
        // Rotate camera relative to Player.
        // Make ThirdCamMaster get World Transform

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        //Transform.rotation = Quaternion.LookRotation(WASD_movement);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(gameObject.transform.forward), 0.15F);

        //Debug.Log("<color=cyan>Target Rotation</color>: <color=cyan>" + targetRotation + "</color>");

        // WASD Rotates character.- TO DO.

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
        
        PlayerRotation();

        //Debug.Log("<color=cyan>Mouse Position: <b>" + Input.mousePosition + "</b></color>");
        
        _playerMovement.y -= 9.81f * Time.deltaTime;
        _charController.Move(_playerMovement * Time.deltaTime);
        // Debug.Log("<color=cyan> " + transform.position + " </color>");
        
        //Debug.Log(playerControls.Movement.WASD.ReadValue<Vector2>().normalized);
        //Debug.Log(WASD_movement);   

    }

    #endregion

    private void PlayerStats()
    {
        
        if (isRunning)
        {

            playerStamina -= 1;

        }

        if (playerStamina == 0)
        {

            isRunning = false;
            playerSpeed = 10f;

        }

    }
    
    float fallTime = 0;
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
        }
        
    }

    private void FixedUpdate()
    {
        
    }

}
