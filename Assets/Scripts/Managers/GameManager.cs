using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject Player;
    [SerializeField] bool lockCurser = true;

    private void Awake()
    {

        Debug.Log("<color=lime><b>Game Manager Script</b> | <i>Assets/Scripts/Player/PlayerMovement.cs</i> | Loaded and Initiated.</color>");
        //Instantiate(Player, transform.position, transform.rotation);

    }

    // Start is called before the first frame update
    void Start()
    {

        if(lockCurser)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

    }

    // Update is called once per frame
    void Update()
    {



    }
}
