using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestGravity : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!_characterController.isGrounded)
        {

            _characterController.Move(new Vector3(0, (-9.81f * Time.deltaTime), 0));

        }
        
    }
}
