using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Managers;

public class PlayerMovementController : MonoBehaviour
{
    // Script Reference
    private CameraFollow _cameraFollow;
    private CharacterController _controller;
    //private PlayerControls _playercontrols;
    private PlayerStats _playerStats;

    [Header("Player Movement Attributes")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJumpHeight = 50;
    [SerializeField] private float _playerRotation = 27;

    private Vector3 _playerMovementDirection;
    
    //[Header("Player Acceleration")]
    //[SerializeField] private float _playerAcceleration = 0f;
    //[SerializeField] private float _playerAccelerationRate = 5f;
    //[SerializeField] private float _playerAcclerationMultiplier = 10f;
    
    private const float _GRAVITY = -9.81f;
    
    private Transform _cameraTransform;
    private Transform _cameraReference;
    
    private Vector3 _lastPos;


    [Header("Public Variables")]
    public float playerVelocity;


    
    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Player Movement Controller Script</b> | <i>Assets/Scripts/Player/PlayerMovementController.cs</i> | Loaded and Initiated.</color>");
        _controller = GetComponent<CharacterController>();
        _playerStats = GetComponent<PlayerStats>();

        gameObject.name = "Player";
        
        //_playercontrols = new PlayerControls();
        //_playercontrols.Movement.Enable();

        _cameraReference = new GameObject().transform;
        _cameraReference.name = "Camera Reference";
        
    }

    private void Start()
    {

        Camera.main.transform.position = transform.position;

        Camera.main.GetComponent<CameraFollow>().enabled = true;
        CameraFollow.Instance.CameraAttach(transform.Find("CameraLookTarget"), transform.Find("CameraLookTarget"));

        _cameraTransform = Camera.main.transform;

        // calculate the correct vertical position:
        float correctHeight = _controller.center.y + _controller.skinWidth;
        // set the controller center vector:
        _controller.center = new Vector3(0, correctHeight, 0);
    }

    private void Update()
    {
        
        PlayerMovement();
        PlayerRotation();


    }

    private void LateUpdate()
    {
                
    }

    private void PlayerMovement()
    {
    //    float _playerWalkingSpeed = 10;
    //    float _playerRunningSpeed = 20;
    //    bool _hasStamina = _playerStats.playerStamina > 0;
    //    bool _isSprinting = _playercontrols.Movement.Sprint.inProgress;
        
    //    Vector3 forward = transform.TransformDirection(Vector3.forward);
    //    Vector3 right = transform.TransformDirection(Vector3.right);

    //    // WASD Movement section.
    //    Vector2 inputVector = _playercontrols.Movement.WASD.ReadValue<Vector2>();
    //    Vector3 _playerMovement = new Vector3(inputVector.x, 0, inputVector.y);
    //    // Move camera relative to Player.
    //    _playerMovement.y = 0;
    //    _playerMovement = _playerMovement.z * _cameraReference.forward.normalized + _playerMovement.x * transform.right.normalized;

    //    //float _playerCurrentSpeedX = (_isSprinting && _hasStamina ? _playerRunningSpeed : _playerWalkingSpeed) * _playerMovement.x;
    //    //float _playerCurrentSpeedZ = (_isSprinting && _hasStamina ? _playerRunningSpeed : _playerWalkingSpeed) * _playerMovement.z;
    //    //float _playerMoveDirectionY = _playerMovementDirection.y;

    //    //_playerMovementDirection = (forward * _playerCurrentSpeedZ) + (right * _playerCurrentSpeedX);

    //    //_playerAcceleration += Time.deltaTime * _playerAcclerationMultiplier;

    //    // Player Sprint
    //    if (_isSprinting)
    //    {
    //        _playerStats.PlayerStaminaHandler();
    //        _playerSpeed = _playerRunningSpeed;
    //    } else
    //    {
    //        _playerSpeed = _playerWalkingSpeed;
    //    }

    //    // Player Jump
    //    if (_controller.isGrounded && _playercontrols.Movement.Jump.triggered)
    //    {
    //        _playerMovement.y = _playerJumpHeight;
    //    }
    //    //else
    //    //{
    //    //    _playerMovementDirection.y = _playerMoveDirectionY;
    //    //}

    //    if (!_controller.isGrounded)
    //    {

    //        _playerMovement.y -= 9.81f * Time.deltaTime;
    //    }

    //    _controller.Move(_playerMovement * Time.deltaTime * _playerSpeed);




    }



    private void PlayerRotation()
    {
        _cameraReference.eulerAngles = new Vector3(0, _cameraTransform.eulerAngles.y, 0);

        //if (Time.deltaTime == 0)
        //{
        //    return;
        //}

        if (_lastPos.x != transform.position.x && _lastPos.z != transform.position.z || Input.GetKey(KeyCode.Mouse0)) 
        {
            Quaternion targetRotation = Quaternion.Euler(0, _cameraReference.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _playerRotation * Time.deltaTime);

        }

        _lastPos.x = transform.position.x;
        _lastPos.z = transform.position.z;
            
    }

}