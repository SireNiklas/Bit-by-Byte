using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    
    Vector3 rot=Vector3.zero;
    float day_cycle=20;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rot.x=day_cycle*Time.deltaTime;
        transform.Rotate(rot,Space.World);
        
    }
}
