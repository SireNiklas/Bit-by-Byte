using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerCollisionsController : MonoBehaviour
{
    private PlayerManager _playerManager;
    
    private void OnTriggerEnter(Collider other)
    {
        SendMessageUpwards("DoVoidDamage");
        
        gameObject.transform.position = _playerManager.PlayerSpawns[Random.Range(0, _playerManager.PlayerSpawns.Count)].transform.position;

    }

}
