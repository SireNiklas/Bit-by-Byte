using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    [SerializeField] private GameObject _projectilePrefab;
    
    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second
 
    private float timestamp;

    private int Itemcount;

    //private Transform rotation;

    // Start is called before the first frame update
    void Start()
    {

        Itemcount = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("ray!");

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        //Ray _rayViewPort = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));

        //Ray _rayMouse = Camera.main.ScreenPointToRay((Input.mousePosition));
        
        //ray = transform.position, transform.forward;
        
        //Debug.DrawRay(ray.origin, ray.direction * 10);

        // if (Input.GetKey(KeyCode.Mouse1) && hit.collider.tag == "Enemy")
        // {
        //     Debug.Log("HIT!");
        //     
        // }

        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        //{

        //}

        if (Physics.Raycast(ray, out hit) && Input.GetKey(KeyCode.Mouse0) && Time.time >= timestamp && hit.collider.tag == "Enemy")
        {
            
            Debug.Log("HIT!");
            GameObject _projectile = Instantiate(_projectilePrefab, Camera.main.transform.TransformPoint(Vector3.forward * 1), Camera.main.transform.rotation);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            Destroy(hit.transform.gameObject);
            
            Itemcount++;

            Debug.Log("<color=yellow>" + Itemcount + "</color>");

            //timestamp = Time.time + timeBetweenShots;

        }


    }
}
