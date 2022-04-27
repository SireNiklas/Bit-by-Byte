using System;
using Cinemachine;
using SirNiklas.Core.Singletons;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class NetworkPlayerMovementController : NetworkBehaviour
{
    
    // Client
    private GameObject _player;
    private CharacterController _charController;
    private PlayerControls _playerControls;
    
    private Transform _cameraTransform;
    private Vector3 _playerMovement;
    
    [SerializeField] private float _playerSpeed = 5;
    [SerializeField] private float _playerJumpHeight = 5;
    [SerializeField] private float _playerRotation = 27f;
    public int playerStamina = 1000;
    private bool _isRunning;
    [SerializeField] private bool _isFirstPerson = true;
    [SerializeField] private bool _isThridPerson = false;
    
    // Network
    private Vector2 _spawnPos = new Vector2(-4, 4);
    private NetworkVariable<Vector3> _networkPositionDirection = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> _networkRotationDirection = new NetworkVariable<Quaternion>();


    private Vector3 _cachedPosition = Vector3.zero;
    private Vector3 _cachedRotation;


    private CameraFollow _CameraFollow;
    public override void OnNetworkSpawn()
    {

        Debug.Log("Something should happen here111111111");
        
        Debug.Log("Client connected" + " Is Client:" + IsClient + "\nIs Host:" + IsHost + "Is Owner:" + IsOwner);

        if (IsClient)
        {
            transform.position = new Vector3(0, 30, 0);
            gameObject.name = "Player " + PlayersManager.Instance.playerCount;

            if (_isFirstPerson)
            {
                Camera.main.GetComponent<CinemachineVirtualCamera>().enabled = true;
                Debug.Log("FirstPersonCAM!!");
                Singleton<CameraFollow>.Instance.FirstPersonFollow(transform.Find("FirstPersonCameraTarget"));
            } else if (_isThridPerson)
            {
                Camera.main.GetComponent<CinemachineFreeLook>().enabled = true;
                Singleton<CameraFollow>.Instance.ThirdPersonFollow(transform.Find("ThirdPersonCameraTarget"));
                Singleton<CameraFollow>.Instance.ThirdPersonLookAt(transform.Find("ThirdPersonCameraTarget"));
            }

        }
        
    }

    private void Awake()
    {
        Debug.Log(
            "<color=lime><b>Network Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        
        _charController = GetComponent<CharacterController>();
        _CameraFollow = GetComponent<CameraFollow>();

        //_playerStats = GetComponent<PlayerStats>();

        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
        _playerControls.Movement.Jump.performed += MovementJump;
        _playerControls.Movement.WASD.performed += MovementWASD;
        _playerControls.Movement.Sprint.started += MovementSprint;
        _playerControls.Movement.Sprint.canceled += MovementSprint;
        
        _cameraTransform = Camera.main.transform;
        
    }

    private void MovementWASD(InputAction.CallbackContext context)
    {

    }

    private void PlayerRotation()
    {
        if (_isFirstPerson)
        {
            transform.rotation = Quaternion.Euler(0, _cameraTransform.transform.eulerAngles.y, 0);
        } else if (_isThridPerson)
        {

            if (_cachedPosition.x != transform.position.x && _cachedPosition.z != transform.position.z)
            {
                // Add Character (look) Rotation dictated by WASD keypress.

                Quaternion targetRotation = Quaternion.Euler(0, _cameraTransform.transform.eulerAngles.y, 0);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, _playerRotation * Time.deltaTime);
            }

            _cachedPosition.x = transform.position.x;
            _cachedPosition.z = transform.position.z;
        }

    }

    private void PlayerMovement(InputAction.CallbackContext context)
    {
        
        Vector2 inputVector = _playerControls.Movement.WASD.ReadValue<Vector2>();
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        
        WASD_movement = WASD_movement.z * _cameraTransform.forward.normalized + WASD_movement.x * transform.right.normalized;
        WASD_movement.y = 0f;

        _charController.Move(WASD_movement * Time.deltaTime * _playerSpeed);
        
        _playerMovement.y -= 9.81f * Time.deltaTime;
        _charController.Move(_playerMovement * Time.deltaTime);
        
        if (_cachedPosition != WASD_movement)
        {
            _cachedPosition = WASD_movement;

        }
        
    }
    
    private void MovementJump(InputAction.CallbackContext context)
    {
        if (_charController.isGrounded)
        {

            _playerMovement.y = _playerJumpHeight;
        }
    }
    
    private void MovementSprint(InputAction.CallbackContext context)
    {
        if (!_isRunning && context.started && playerStamina > 0)
        {
            _playerSpeed = _playerSpeed *= 2f;
            _isRunning = true;

        } else if (_isRunning && context.canceled)
        {

            _playerSpeed = _playerSpeed /=2;
            _isRunning = false;
            
        }

    }
    
    private void SprintHandler()
    {
        MovementSprint(default);
        if (_isRunning)
        {
            
            //_playerStats.PlayerStaminaHandler();

        }
        
        if (_isRunning && playerStamina < 1)
        {
            
            _playerSpeed = _playerSpeed /= 2f;
            _isRunning = false;
            
        }

    }

    private void ClientInput(InputAction.CallbackContext context)
    {
        
        PlayerMovement(default);
        PlayerRotation();
        MovementSprint(default);
        SprintHandler();
        
        // Update server
        //UpdateClientTransformServerRPC();

    }
    
    [ServerRpc]
    public void UpdateClientTransformServerRPC()
    {
        //_networkPositionDirection.Value = transform.position;
        //_networkRotationDirection.Value = transform.rotation;

        _networkPositionDirection.Value = Vector3.one;
        _networkRotationDirection.Value = quaternion.identity;

    }
    
    private void ClientMove()
    {
        if (_networkPositionDirection.Value != Vector3.zero)
        {
            _charController.Move(_networkPositionDirection.Value * _playerSpeed * Time.deltaTime);
        }
    }
    
    private void Update()
    {
        if (IsClient && IsOwner)
        {
            ClientInput(default);

        }
        ClientMove();
    }
    
}
