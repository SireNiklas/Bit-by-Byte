using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    
    //public GameObject Player;
    [SerializeField] bool lockCurser = true;
    
    [SerializeField] private GameObject _sun;

    [SerializeField] private bool _isPaused = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        if (lockCurser)
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

        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape) && _isPaused == false)
        {

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _isPaused = true;

        } else if (Input.GetKey(KeyCode.Escape) && _isPaused == true)
        {
         
            Time.timeScale = 1;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _isPaused = false;
            
        }

    }
}
