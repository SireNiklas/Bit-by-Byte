using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{

    private CharacterController charController;

    public GameObject Player;

    private Vector3 playerMovement;
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float playerJump = 3f;
    private Vector3 GRAVITY = Physics.gravity;

    private Transform cameraTransform;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {

        Debug.Log("<color=lime><b>Player Movement Script</b> | <i>Assets/Scripts/Player/PlayerMovement.cs</i> | Loaded and Initiated.</color>");
        charController = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;

        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerMovement.Jump.performed += MovementJump;
        playerInputActions.PlayerMovement.WASD.performed += MovementWASD;
    }

    private void MovementWASD(InputAction.CallbackContext context)
    {

        Debug.Log("<color=cyan>Player has initiated the <b>WASD</b> command.</color>");

    }

    public void MovementJump(InputAction.CallbackContext context)
    {

        Debug.Log("<color=cyan>Player has initiated the <b>Jump</b> command.\n</color>" + context.phase);
        if (charController.isGrounded)
        {

            playerMovement.y = playerJump;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // WASD Movement section.
        Vector2 inputVector = playerInputActions.PlayerMovement.WASD.ReadValue<Vector2>().normalized;
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        // Rename WASD_movement - to something.

        // Move camera relative to Player.
        WASD_movement = WASD_movement.x * cameraTransform.right.normalized + WASD_movement.z * cameraTransform.forward.normalized;
        WASD_movement.y = 0f;

        charController.Move(WASD_movement * Time.deltaTime * playerSpeed);

        //Debug.Log("<color=cyan>Mouse Position: <b>" + Input.mousePosition + "</b></color>");

        playerMovement.y -= 9.81f * Time.deltaTime;
        charController.Move(playerMovement * Time.deltaTime);
        // Debug.Log("<color=cyan> " + transform.position + " </color>");

    }

    void LateUpdate()
    {

        // Rotate camera relative to Player.
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

    }

    void FixedUpdate()
    {


        
    }
}
