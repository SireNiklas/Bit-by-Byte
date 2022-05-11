using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackTypes", menuName = "ScriptableObjects/PlayerAttacks")]
public class PlayerActionsSO : ScriptableObject
{

    public Transform bulletSpawn;
    public TrailRenderer bulletTrail;
    
}
