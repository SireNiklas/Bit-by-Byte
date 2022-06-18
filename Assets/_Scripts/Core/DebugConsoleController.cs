using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    [SerializeField]
    private GameObject _enemyPrefab;
    
    [SerializeField]
    private TMP_InputField _textField;
    
    private string _inputText;
    
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

    private void CMDInput()
    {
        
        if (Input.GetKeyUp(KeyCode.Return))
        {
            
            _inputText = _textField.text;

            string CMDPrefix = "/";
            string CMDContent = "";
            
            int CMDArg = 0;
            
            var CMD = CMDPrefix + CMDContent + CMDArg;
            
            if (_inputText.StartsWith(CMDPrefix))
            {

                var CMDParse = Regex.Match(CMD, @"\d+$");

                if (CMDParse.Success)
                {
                    
                    Debug.Log(CMDParse);
                    SpawnEnemy(CMDArg);

                }
                else
                {
                    
                }

                _textField.text = "";

            }

            _textField.text = "";

        }
        
    }
    
    /*private int CMDParseInt(string CMD)
    {
        return;
    }*/

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
    
    // COMMANDS FUNCTIONS:
    
    private void SpawnEnemy(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            GameObject _enemy = Instantiate(_enemyPrefab, new Vector3(0, 1, 0), transform.rotation);
        }
    }
    
    List<GameObject> enemies;
    
    private void KillEnemy()
    {

        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.ForEach(Destroy);

    }
    
}