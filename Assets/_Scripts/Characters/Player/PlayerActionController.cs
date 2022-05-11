using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField]
    private Transform BulletSpawnPoint;
    //[SerializeField]
    //private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private LayerMask Mask;
    
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
            
            if (Physics.Raycast(Camera.main.transform.position,
                    Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).direction, out hit, Single.MaxValue, Mask));
            {
                Debug.Log("HIT!");

                if (hit.collider != null)
                {

                    //hit.collider.enabled = false;
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
