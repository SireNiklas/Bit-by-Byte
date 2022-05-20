using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    
    [SerializeField]
    private Button _startButton, _optionsMenu, _endButton;

    // Start is called before the first frame update
    void Start()
    {
        
        _startButton.onClick.AddListener(StartGame);
        _endButton.onClick.AddListener(EndGame);

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void StartGame()
    {
        SceneManager.LoadScene("ProtoWorld");

    }

    private void GameOptions()
    {
        
        
    }

    private void EndGame()
    {
        
        Application.Quit();
        
    }
}
