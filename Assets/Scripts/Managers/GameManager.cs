using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    //public GameObject Player;
    //[SerializeField] bool lockCurser = true;
    public static bool IsFirstPActive;
    public static bool IsThirdPActive;

    private void Awake()
    {

        Debug.Log("<color=lime><b>Game Manager Script</b> | <i>Assets/Scripts/Player/PlayerMovement.cs</i> | Loaded and Initiated.</color>");
        //Instantiate(Player, transform.position, transform.rotation);

        /*if (lockCurser)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        if (Gamepad.all.Count > 0)
        {

            Debug.Log(Gamepad.current);

        } else
        {

            Debug.Log("<color=cyan>No gamepads detected</color>");

        }*/

        /*if (FirstPersonController.IsFirstPerson == true)
        {

            IsFirstPActive = true;
            IsThirdPActive = false;

        } else if (ThirdPersonController.IsThirdPerson == true)
        {

            IsThirdPActive = true;
            IsFirstPActive = false;

        }*/

    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
        

    }
}
