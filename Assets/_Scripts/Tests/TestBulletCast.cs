using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestBulletCast : MonoBehaviour
{
    //[SerializeField]
    //private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private Transform BulletSpawnPoint;
    //[SerializeField]
    //private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private LayerMask Mask;
    
    //private float LastShootTime;
    private float timestamp;
    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second
    
    private void Awake()
    {
    }

    private void Update()
    {
        
        
    }
    

}