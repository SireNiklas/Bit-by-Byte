using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMovementController : NetworkBehaviour
{
    
    private GameObject player;
    private CharacterController _charController;
    private PlayerControls _playercontrols;

    PlayerStats _playerStats;
    
    [SerializeField] private float _playerspeed = 5;
    
    // Network
    [SerializeField] private Vector2 defaultpositionrange = new Vector2(-4, 4);
    [SerializeField] private NetworkVariable<float> forwardbackwardpos = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<float> leftrightpos = new NetworkVariable<float>();
    
    // Client caching
    private float oldforwardbackpos;
    private float oldleftrightpos;


    [SerializeField] private float _playerjumpheight = 5;
    
    public int playerstamina = 1000;
    

    private Vector3 _playerMovement;

    [SerializeField] private float playerRotation;
    
    [SerializeField] public bool isRunning;

    private Transform _cameratransform;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        _charController = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();

        player = GameObject.Find("FirstPerson");

        _cameratransform = Camera.main.transform;

        _playercontrols = new PlayerControls();
        _playercontrols.Movement.Enable();
        _playercontrols.Movement.Jump.performed += MovementJump;
        _playercontrols.Movement.WASD.performed += MovementWASD;
        _playercontrols.Movement.Sprint.started += MovementSprint;
        _playercontrols.Movement.Sprint.canceled += MovementSprint;
        
    }
    
    private void Start()
    {

        transform.position = new Vector3(Random.Range(defaultpositionrange.x, defaultpositionrange.y), 0,
        Random.Range(defaultpositionrange.x, defaultpositionrange.y));
    }

    private void UpdateServer()
    {
        transform.position = new Vector3(transform.position.x + leftrightpos.Value, transform.position.y,
            transform.position.z + forwardbackwardpos.Value);

    }

    private void UpdateClient()
    {

        float forwardbackward = 0;
        float leftright = 0;
        
        Vector2 inputVector = _playercontrols.Movement.WASD.ReadValue<Vector2>();
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        
        WASD_movement = WASD_movement.z * _cameratransform.forward.normalized + WASD_movement.x * transform.right.normalized;
        WASD_movement.y = 0f;

        _charController.Move(WASD_movement * Time.deltaTime * _playerspeed);
        
        _playerMovement.y -= 9.81f * Time.deltaTime;
        _charController.Move(_playerMovement * Time.deltaTime);

        if (oldforwardbackpos != forwardbackward || oldleftrightpos != leftright)
        {
            oldforwardbackpos = forwardbackward;
            oldleftrightpos = leftright;
            // Update server
            UpdateClientPositionServerRPC(forwardbackward, leftright);

        }

    }

    [ServerRpc]
    public void UpdateClientPositionServerRPC(float forwardbackward, float leftright)
    {
        forwardbackwardpos.Value = forwardbackward;
        leftrightpos.Value = leftright;

    }

    private void MovementWASD(InputAction.CallbackContext context)
    {

    }

    private void MovementSprint(InputAction.CallbackContext context)
    {
        if (!isRunning && context.started && playerstamina > 0)
        {
            _playerspeed = _playerspeed *= 2f;
            isRunning = true;

        } else if (isRunning && context.canceled)
        {

            _playerspeed = _playerspeed /=2;
            isRunning = false;
            
        }

    }

    private void SprintHandler()
    {
        
        if (isRunning)
        {
            
            //_playerStats.PlayerStaminaHandler();

        }
        
        if (isRunning && isRunning && playerstamina < 1)
        {
            
            _playerspeed = _playerspeed /= 2f;
            isRunning = false;
            
        }

    }

    private void MovementJump(InputAction.CallbackContext context)
    {
        if (_charController.isGrounded)
        {

            _playerMovement.y = _playerjumpheight;
        }
    }
    
    private void Update()
    {
        
        //PlayerMovement();
        //SprintHandler();
        
        if (IsServer)
        {
            UpdateServer();

        }

        if (IsClient && IsOwner)
        {
            UpdateClient();
            
        }

    }

    /*   private void LateUpdate()
   {
       PlayerRotation();
   }

    private void PlayerRotation()
   {

       if (_firstpcam.activeSelf)
       {
           // Rotate camera relative to Player.
           // Make ThirdCamMaster get World Transform

           //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
           transform.rotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);

       } else if (_thirdpcam.activeSelf) 
       {

           if (lastPos.x != transform.position.x && lastPos.z != transform.position.z)
           {
               // Add Character (look) Rotation dictated by WASD keypress.

               Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
               transform.rotation =
                   Quaternion.Slerp(transform.rotation, targetRotation, playerRotation * Time.deltaTime);
           }

           lastPos.x = transform.position.x;
           lastPos.z = transform.position.z;

       }

       //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
       //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

       //Transform.rotation = Quaternion.LookRotation(WASD_movement);
       //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(gameObject.transform.forward), 0.15F);

       //Debug.Log("<color=cyan>Target Rotation</color>: <color=cyan>" + targetRotation + "</color>");

       // WASD Rotates character.- TO DO.

   }*/

    private void PlayerMovement()
    {

        /*
         * Fix Player speed dictated by the Camera angle.
         * Either Rotate player on A and D press, or animate a strafe.
         */
        
        // WASD Movement section.
        //Vector2 inputVector = _playercontrols.Movement.WASD.ReadValue<Vector2>();
        //Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        // Rename WASD_movement - to something.
        
        // Move camera relative to Player.
        //WASD_movement = WASD_movement.z * _cameratransform.forward.normalized + WASD_movement.x * transform.right.normalized;
        //WASD_movement.y = 0f;

        //_charController.Move(WASD_movement * Time.deltaTime * _playerspeed);
        
        //Debug.Log("<color=cyan>Mouse Position: <b>" + Input.mousePosition + "</b></color>");
        
        //_playerMovement.y -= 9.81f * Time.deltaTime;
        //_charController.Move(_playerMovement * Time.deltaTime);
        // Debug.Log("<color=cyan> " + transform.position + " </color>");
        
        //Debug.Log(playerControls.Movement.WASD.ReadValue<Vector2>().normalized);
        //Debug.Log(WASD_movement);   

    }
    
}