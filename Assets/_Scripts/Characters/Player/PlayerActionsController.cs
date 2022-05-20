using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsController : MonoBehaviour
{
    [SerializeField]
    private Transform BulletSpawnPoint;
    //[SerializeField]
    //private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private LayerMask Mask;

    public AudioSource _handObject;
    
    private float timestamp;
    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Shoot();

    }

    private void Shoot()
    {
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= timestamp)
        {
            // Use an object pool instead for these! To keep this tutorial focused, we'll skip implementing one.
            // For more details you can see: https://youtu.be/fsDE_mO4RZM or if using Unity 2021+: https://youtu.be/zyzqA_CPz2E
            
            //Ray ray = new Ray(transform.Find("CameraLookTarget").TransformPoint(Vector3.forward * 1),
            //Camera.main.transform.forward, out RaycastHit hit, float.MaxValue, Mask);

            //Ray ray = new Ray(transform.Find("CameraLookTarget").position);
            
            _handObject.Play();

            //if (Physics.Raycast(BulletSpawnPoint.position, Vector3.forward, ))
            
            //if (Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)).direction, out hit, Single.MaxValue, Mask));

            Transform _hitTransform = null;

            RaycastHit _cameraHit;
            
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _cameraHit, 999f, Mask))
            {

                Vector3 _shootDirection = _cameraHit.point - BulletSpawnPoint.position;

                if (Physics.Raycast(BulletSpawnPoint.position, _shootDirection, out hit, 999f, Mask))
                {

                    _hitTransform = hit.transform;
                
                    //Debug.DrawRay(BulletSpawnPoint.position, _shootDirection, Color.red, 20f);
                
                    // Check if ray is obstructed from bullet spawn.
                
                    if (_hitTransform != null)
                    {
                
                        //Debug.DrawRay(BulletSpawnPoint.position, Camera.main.transform.forward * 999f, Color.green, 5f);
                        //Debug.DrawRay(BulletSpawnPoint.position, hit.point * 10f, Color.green, 5f);
                        //Debug.DrawLine(BulletSpawnPoint.position + Vector3.up * .1f, hit.point + Vector3.up * .1f, Color.green, 20f);

                    
                        TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                        StartCoroutine(SpawnTrail(trail, hit));

                        if (hit.transform.CompareTag("Enemy"))
                        {
                            hit.transform.SendMessageUpwards("HitByRay");

                        }
                    }

                    timestamp = Time.time + timeBetweenShots;

                }
                
                
            }
            
        }
        
    }
    
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hitInput)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hitInput.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        Trail.transform.position = hitInput.point;
        //Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }
}
