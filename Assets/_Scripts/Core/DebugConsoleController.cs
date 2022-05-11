using System.Security.Cryptography.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Transform = UnityEngine.Transform;
using TMPro;

public class DebugConsoleController : MonoBehaviour
{

    //EventSystem.current.IsPointerOverGameObject() returns true if the mouse is over an UI element.
    
    // EventSystem.current.IsPointerOverGameObject() == false
    
    [SerializeField] private bool _isShown = false;
    
    private Transform _Canvas;
    
    private float timestamp;
    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second
    
    private void Awake()
    {

        Debug.Log("Controler for Console");

    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        DebugConsoleToggle();
        //if (EventSystem.current.IsPointerOverGameObject()) return;

    }

    private void DebugConsoleToggle()
    {
        if (Input.GetKey(KeyCode.BackQuote) && Time.time >= timestamp)
        {
            if (_isShown == false)
            {
                _isShown = true;
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);

                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                
                timestamp = Time.time + timeBetweenShots;

                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = true;
                
            }
            else if (_isShown == true)
            {

                _isShown = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                
                Cursor.lockState = CursorLockMode.Locked;

                Cursor.visible = false;
            }
            
            timestamp = Time.time + timeBetweenShots;

        }
        
    }
}