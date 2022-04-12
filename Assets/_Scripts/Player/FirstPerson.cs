using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    private Transform cameraTransform;

    private void Awake()
    {

        Debug.Log(
            "<color=lime><b>Third Person Controller Script</b> | <i>Assets/Scripts/Player/ThirdPersonController.cs</i> | Loaded and Initiated.</color>");
        
        cameraTransform = Camera.main.transform;
    }

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerRotation();

    }
    
    private void PlayerRotation()
    { 
        // Rotate camera relative to Player.
        // Make ThirdCamMaster get World Transform

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.transform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        //Transform.rotation = Quaternion.LookRotation(WASD_movement);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(gameObject.transform.forward), 0.15F);

        //Debug.Log("<color=cyan>Target Rotation</color>: <color=cyan>" + targetRotation + "</color>");

        // WASD Rotates character.- TO DO.

    }
}
