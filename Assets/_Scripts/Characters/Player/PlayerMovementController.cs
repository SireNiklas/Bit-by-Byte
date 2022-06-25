using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerMovementController : MonoBehaviour
{
    private GameManager _gameManager;
    
    private CameraFollow _cameraFollow;
    
    private CharacterController _charController;
    private PlayerControls _playercontrols;
    
    private Animator _animator;
    
    PlayerStats _playerStats;
    
    [Header("Player Movement Attributes")]
    [SerializeField] private float _playerWalkingSpeed = 10;
    [SerializeField] private float _playerRunningSpeed = 20;
    [SerializeField] private float _playerJumpHeight = 50;
    [SerializeField] private float _playerRotation = 27;

    private Vector3 _playerMovementDirection;
    
    [Header("Player Acceleration")]
    [SerializeField] private float _playerAcceleration;
    [SerializeField] private float _playerAccelerationRate = 5f;
    [SerializeField] private float _playerAcclerationMultiplier = 10f;
    
    private const float _GRAVITY = -9.81f;

    private Vector3 _playerMovement;
    
    private Transform _cameraTransform;
    private Transform _cameraReference;
    
    private Vector3 _lastPos;
    
    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        _charController = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();

        gameObject.name = "Player";
        
        _playercontrols = new PlayerControls();
        _playercontrols.Movement.Enable();

        _cameraReference = new GameObject().transform;
        _cameraReference.name = "Camera Reference";
        

    }

    private void Start()
    {

        Camera.main.transform.position = transform.position;

            Camera.main.GetComponent<CameraFollow>().enabled = true;
            CameraFollow.Instance.CameraAttach(transform.Find("CameraLookTarget"), transform.Find("CameraLookTarget"));

            _cameraTransform = Camera.main.transform;

                
            _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        
        PlayerMovement();
        
    }

    private void LateUpdate()
    {
        
        PlayerRotation();
        
    }

    private void PlayerMovement()
    {

        bool hasStamina = _playerStats.playerStamina > 0;

        float _playerMovementVelocityY = 0;
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // WASD Movement section.
        Vector2 inputVector = _playercontrols.Movement.WASD.ReadValue<Vector2>();
        Vector3 _playerMovement = new Vector3(inputVector.x, 0, inputVector.y);

        float _playerCurrentSpeedX = (_playercontrols.Movement.Sprint.inProgress && hasStamina ? _playerRunningSpeed : _playerWalkingSpeed) * _playerMovement.x;
        float _playerCurrentSpeedZ = (_playercontrols.Movement.Sprint.inProgress && hasStamina ? _playerRunningSpeed : _playerWalkingSpeed) * _playerMovement.z;

        float _playerMoveDirectionY = _playerMovementDirection.y;
        
        _playerMovementDirection = (forward * _playerCurrentSpeedZ) + (right * _playerCurrentSpeedX);
            
        // Move camera relative to Player.
        _playerMovement = _playerMovement.z * _cameraReference.forward.normalized + _playerMovement.x * transform.right.normalized;
        

            // _playerAcceleration += _playerAccelerationRate * Time.deltaTime * _playerAcclerationMultiplier;
            // _playerAcceleration = Mathf.Min(_playerAcceleration, _playerSpeed);
        
            // _playerAcceleration -= _playerAccelerationRate * Time.deltaTime * 2;
            // _playerAcceleration = Mathf.Max(_playerAcceleration, 0);
            
        if (_playerCurrentSpeedX == 0f && _playerCurrentSpeedZ == 0f)
        {
            
            //_animator.SetFloat("playerMovementState", 0);
            //_animator.SetFloat("playerWalkingState", 0);
            //_animator.speed = 1;
        }
        else
        {

            //_animator.speed = 5;

        }
        
        if (_playerCurrentSpeedX < 0)
        {
            
            //_animator.SetFloat("speed", 1);
            
        } else if (_playerCurrentSpeedX > 0)
        {
            
            //_animator.SetFloat("walking", 0.42f);
        
        }

        if (_playerCurrentSpeedZ < 0)
        {
            
            //_animator.SetFloat("walking", 0.38f);

        }

        // Player Sprint
        else if (_playercontrols.Movement.Sprint.inProgress)
        {
            _playerStats.PlayerStaminaHandler();
            //_animator.SetFloat("speed", 1f);
            //_animator.speed = 10;
        }
        
        // Player Jump
        if (_charController.isGrounded && _playercontrols.Movement.Jump.triggered)
        {
            _playerMovementDirection.y = _playerJumpHeight;
            //_animator.SetBool("isJumping", true);
        } else if (_charController.isGrounded)
        {
            //_animator.SetBool("isJumping", false);

        }
        else
        { 
            
            _playerMovementDirection.y = _playerMoveDirectionY;
            //_animator.speed = 1f;

        }

        //Debug.Log(_animator.GetBool("isJumping"));
        //Debug.Log(_animator.GetFloat("speed"));
        //Debug.Log((_playerCurrentSpeedX, _playerCurrentSpeedZ));

        if (!_charController.isGrounded)
        {

            _playerMovementDirection.y -= 9.81f * Time.deltaTime;
            // float _playerInertia = 0;
            //
            // if (_playerInertia != _playerSpeed)
            // {
            //
            //     _playerInertia += 1;
            //
            // }

            //_charController.Move(_playerMovement * Time.deltaTime);
        }

        _charController.Move(_playerMovementDirection * Time.deltaTime);

    }

    private void PlayerRotation()
    {
        _cameraReference.eulerAngles = new Vector3(0, _cameraTransform.eulerAngles.y, 0);

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

}