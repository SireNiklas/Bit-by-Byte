using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovementController : MonoBehaviour
{
    private CameraFollow _cameraFollow;
    
    private CharacterController _charController;
    private PlayerControls _playercontrols;

    PlayerStats _playerStats;
    
    [Header("Player Attributes")]
    [SerializeField] private float _playerSpeed = 5;
    [SerializeField] private float _playerJumpHeight = 50;
    [SerializeField] private float _playerRotation = 27;
    public int playerStamina = 1000;
    
    private const float _GRAVITY = -9.81f;

    private Vector3 _playerMovement;
    
    [Header("Player Checks")]
    [SerializeField] public bool isRunning;

    private Transform _cameratransform;
    private Transform _cameraReference;
    
    private Vector3 _lastPos;
    
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    [SerializeField] List<GameObject> PlayerSpawns;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        _charController = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();

        gameObject.name = "Player";
        
        _playercontrols = new PlayerControls();
        _playercontrols.Movement.Enable();
        _playercontrols.Movement.Jump.performed += MovementJump;
        _playercontrols.Movement.WASD.performed += MovementWASD;
        _playercontrols.Movement.Sprint.started += MovementSprint;
        _playercontrols.Movement.Sprint.canceled += MovementSprint;

        _cameraReference = new GameObject().transform;
        _cameraReference.name = "Camera Reference";

    }

    private void Start()
    {

        Camera.main.transform.position = transform.position;

            Camera.main.GetComponent<CameraFollow>().enabled = true;
            CameraFollow.Instance.CameraAttach(transform.Find("CameraLookTarget"), transform.Find("CameraLookTarget"));

            _cameratransform = Camera.main.transform;
        
    }

    private void Update()
    {
        
        PlayerMovement();
        SprintHandler();

    }

    private void LateUpdate()
    {
        
        PlayerRotation();
        
    }

    private void PlayerMovement()
    {

        // WASD Movement section.
        Vector2 inputVector = _playercontrols.Movement.WASD.ReadValue<Vector2>();
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        // Rename WASD_movement - to something.
        
        // Move camera relative to Player.
        WASD_movement = WASD_movement.z * _cameraReference.forward.normalized + WASD_movement.x * transform.right.normalized;
        WASD_movement.y = 0f;

        _charController.Move(WASD_movement * Time.deltaTime * _playerSpeed);
        
        if (!_charController.isGrounded)
        {
            _playerMovement.y -= 9.81f * Time.deltaTime;
            _charController.Move(_playerMovement * Time.deltaTime);
        }
    }
    
    private void PlayerRotation()
    {
        _cameraReference.eulerAngles = new Vector3(0, _cameratransform.eulerAngles.y, 0);

        if (Time.deltaTime == 0)
        {
            return;
        }

        if (_lastPos.x != transform.position.x && _lastPos.z != transform.position.z || Input.GetKey(KeyCode.Mouse0)) 
        {
            Quaternion targetRotation = Quaternion.Euler(0, _cameraReference.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _playerRotation * Time.deltaTime);
                
        }

        _lastPos.x = transform.position.x;
        _lastPos.z = transform.position.z;
            
    }

    private void MovementWASD(InputAction.CallbackContext context)
    {

    }

    private void MovementSprint(InputAction.CallbackContext context)
    {
        if (!isRunning && context.started && playerStamina > 0)
        {
            _playerSpeed = _playerSpeed *= 2f;
            isRunning = true;

        } else if (isRunning && context.canceled)
        {

            _playerSpeed = _playerSpeed /=2;
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
            
            _playerSpeed = _playerSpeed /= 2f;
            isRunning = false;
            
        }

    }

    private void MovementJump(InputAction.CallbackContext context)
    {
        if (_charController.isGrounded)
        {

            _playerMovement.y = _playerJumpHeight;
        }
    }
    
}