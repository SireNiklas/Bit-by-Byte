using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionsController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport!");

        SceneManager.LoadScene("ProtoWorld");

    }

}
