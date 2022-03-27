using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private CharacterController charController;

    private Vector3 playerMovement;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJump;
    [SerializeField] public static int playerHealth = 100;
    [SerializeField] public static int playerStamina = 100;

    [SerializeField] private float rotationSpeed = .8f;

    public GameObject ExtPersonCam;
    public GameObject IntPersonCam;
    public GameObject ThirdCamMaster;

    public GameObject ExtUI;
    public GameObject IntUI;


    public float regenRate;

    private float playerStatDelay;

    [SerializeField] private bool isRunning;

    private Vector3 GRAVITY = Physics.gravity;

    private Vector3 lastPos;

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
        playerInputActions.PlayerMovement.Sprint.started += MovementSprint;
        playerInputActions.PlayerMovement.Sprint.canceled += MovementSprint;
    }

    public void Start()
    {
        InvokeRepeating("Regenerate", 0.0f, regenRate);
    }

    #region Movement System

    private void MovementWASD(InputAction.CallbackContext context)
    {

        Debug.Log("<color=cyan>Player has initiated the <b>WASD</b> command.\n</color>" + context.phase);
    }

    private void MovementSprint(InputAction.CallbackContext context)
    {

        Debug.Log("<color=cyan>Player has initiated the <b>Sprint</b> command.\n</color>" + context.phase);

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
            Debug.Log("<color=cyan>Player has stopped the <b>Sprint</b> command.\n</color>" + context.phase);

        }

    }

    private void MovementJump(InputAction.CallbackContext context)
    {

        Debug.Log("<color=cyan>Player has initiated the <b>Jump</b> command.\n</color>" + context.phase);
        if (charController.isGrounded)
        {

            playerMovement.y = playerJump;
        }
    }
    #endregion

    void Regenerate()
    {
        if (playerStamina < 100)
            playerStamina++;
    }

    // Update is called once per frame
    void Update()
    {

        // WASD Movement section.
        Vector2 inputVector = playerInputActions.PlayerMovement.WASD.ReadValue<Vector2>().normalized;
        Vector3 WASD_movement = new Vector3(inputVector.x, 0, inputVector.y);
        // Rename WASD_movement - to something.

        // Move camera relative to Player.
        WASD_movement = WASD_movement.x * transform.right.normalized + WASD_movement.z * transform.forward.normalized;
        WASD_movement.y = 0f;

        //Transform.rotation = Quaternion.LookRotation(WASD_movement);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(WASD_movement), 0.15F);

        charController.Move(WASD_movement * Time.deltaTime * playerSpeed);

        //Debug.Log("<color=cyan>Mouse Position: <b>" + Input.mousePosition + "</b></color>");

        playerMovement.y -= 9.81f * Time.deltaTime;
        charController.Move(playerMovement * Time.deltaTime);
        // Debug.Log("<color=cyan> " + transform.position + " </color>");

        //Debug.Log(playerInputActions.PlayerMovement.WASD.ReadValue<Vector2>().normalized);

        if (isRunning)
        {

            playerStamina -= 1;

        }

        if (playerStamina == 0)
        {

            isRunning = false;
            playerSpeed = 10f;

        }

        if (Input.GetKeyDown(KeyCode.C) && ExtPersonCam.activeSelf)
        {

            ExtPersonCam.SetActive(false);
            ExtUI.SetActive(false);
            IntPersonCam.SetActive(true);
            IntUI.SetActive(true);

        } else if (Input.GetKeyDown(KeyCode.C) && IntPersonCam.activeSelf)
        {

            IntPersonCam.SetActive(false);
            IntUI.SetActive(false);
            ExtPersonCam.SetActive(true);
            ExtUI.SetActive(true);
        }
    }

    void LateUpdate()
    {

        if (ExtPersonCam.activeSelf)
        {

            if (lastPos.x != gameObject.transform.position.x && lastPos.z != gameObject.transform.position.z)
            {

                // Rotate camera relative to Player.
                // Make ThirdCamMaster get World Transform

                //Quaternion targetRotation = Quaternion.Euler(0, ThirdCamMaster.transform.eulerAngles.y, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 18.75f * Time.deltaTime);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward, transform.up), 0.15F);


                Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

                Debug.Log("<color=cyan>Target Rotation</color>: <color=cyan>" + targetRotation + "</color>");
            }

            lastPos = gameObject.transform.position;
        } else if (IntPersonCam.activeSelf) {

           //transform.rotation.y = Camera.main.transform.rotation.y;

        }

    }

    void FixedUpdate()
    {


        
    }
}