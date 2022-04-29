using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovementController : MonoBehaviour
{
    private CameraFollow _cameraFollow;
    
    private CharacterController _charController;
    private PlayerControls _playercontrols;

    PlayerStats _playerStats;
    
    [SerializeField] private float _playerspeed = 5;
    [SerializeField] private float _playerjumpheight = 5;
    [SerializeField] private float _playerRotation = 27;

    public int playerstamina = 1000;
    

    private Vector3 _playerMovement;

    [SerializeField] private float playerRotation;
    
    [SerializeField] public bool isRunning;

    [SerializeField] private bool _isFirstPerson = true;
    [SerializeField] private bool _isThirdPerson;

    private Transform _cameratransform;
    
    private Vector3 _lastPos;

    public Transform PlayerView;
    
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        _charController = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();
        
        _playercontrols = new PlayerControls();
        _playercontrols.Movement.Enable();
        _playercontrols.Movement.Jump.performed += MovementJump;
        _playercontrols.Movement.WASD.performed += MovementWASD;
        _playercontrols.Movement.Sprint.started += MovementSprint;
        _playercontrols.Movement.Sprint.canceled += MovementSprint;

        if (_isFirstPerson)
        {
            Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
            Camera.main.GetComponent<CameraFollow>().enabled = true;
            CameraFollow.Instance.CameraFirstPerson(transform.Find("FirstPersonCameraLookTarget"));
        }
        
        _cameratransform = Camera.main.transform;

        // if (_isFirstPerson)
        // {
        //     
        //     Debug.Log("<color=red>CAMERA DURING VIRTUAL!</color>");
        //
        // } else if (_isThirdPerson)
        // {
        //     CameraFollow.Instance.CameraThirdPerson(transform.Find("ThirdPersonCameraLookTarget"), transform.Find("ThirdPersonCameraLookTarget"));
        //
        // }
        
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
            
            _playerStats.PlayerStaminaHandler();

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
        
        PlayerMovement();
        SprintHandler();
        PlayerRotation();

    }

    private void LateUpdate()
    {
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
    
    private void PlayerRotation()
    { 
        // Rotate camera relative to Player.
        // Make ThirdCamMaster get World Transform

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
        if (_isFirstPerson)
        {

            transform.rotation = Quaternion.Euler(0, _cameratransform.transform.eulerAngles.y, 0);
        } else if (_isThirdPerson)
        {
            
            if (_lastPos.x != transform.position.x && _lastPos.z != transform.position.z)
            {
                // Add Character (look) Rotation dictated by WASD keypress.
            
                Quaternion targetRotation = Quaternion.Euler(0, _cameratransform.transform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _playerRotation * Time.deltaTime);
            }

            _lastPos.x = transform.position.x;
            _lastPos.z = transform.position.z;
            
        }
        
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
        Vector2 inputVector = _playercontrols.Movement.WASD.ReadValue<Vector2>();
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        // Rename WASD_movement - to something.
        
        // Move camera relative to Player.
        WASD_movement = WASD_movement.z * _cameratransform.forward.normalized + WASD_movement.x * transform.right.normalized;
        WASD_movement.y = 0f;

        _charController.Move(WASD_movement * Time.deltaTime * _playerspeed);
        
        //Debug.Log("<color=cyan>Mouse Position: <b>" + Input.mousePosition + "</b></color>");
        
        _playerMovement.y -= 9.81f * Time.deltaTime;
        _charController.Move(_playerMovement * Time.deltaTime);
        // Debug.Log("<color=cyan> " + transform.position + " </color>");
        
        //Debug.Log(playerControls.Movement.WASD.ReadValue<Vector2>().normalized);
        //Debug.Log(WASD_movement);   

    }
    
}