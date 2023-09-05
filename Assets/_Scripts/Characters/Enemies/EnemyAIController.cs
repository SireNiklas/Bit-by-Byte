using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _enemy;
    public GameObject _player;

    private void Awake()
    {

        _enemy = GetComponent<NavMeshAgent>();

        _enemy.Warp(new Vector3(0,1,0));

    }
    

    private void Start()
    {
        
    }

    private void Update()
    {

        _enemy.SetDestination(_player.transform.position);

        
    }
}
