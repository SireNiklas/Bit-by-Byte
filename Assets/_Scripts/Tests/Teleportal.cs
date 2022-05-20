using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.FindWithTag("Player");

    }
}
