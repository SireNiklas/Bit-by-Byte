using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        SceneManager.LoadSceneAsync("ProtoWorld", LoadSceneMode.Additive);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
