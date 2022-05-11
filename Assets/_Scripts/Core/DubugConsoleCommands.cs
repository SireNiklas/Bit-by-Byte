using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class DubugConsoleCommands : MonoBehaviour
{
    private CinemachineFreeLook _cinemachineFreeLook;
    
    //[SerializeField] private TextMeshProUGUI _TextField;

    [SerializeField]
    private TMP_InputField _textField;

    private float InputValue1;
    private float InputValue2;
    
    private string _inputText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        SubmitText();

    }

    private void SubmitText()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            
            _inputText = _textField.text;

            Debug.Log(_inputText);

            if (_inputText == "/camerasense" + InputValue1 + InputValue2)
            {
                Debug.Log("PASSED!");
                
                CameraSense(InputValue1, InputValue2);

            }
            
            _textField.text = "";

        }

    }

    private void CameraSense(float senseValueX, float senseValueY)
    {

        _cinemachineFreeLook.m_XAxis.m_MaxSpeed = senseValueX;
        _cinemachineFreeLook.m_YAxis.m_MaxSpeed = senseValueY;

    }
}
