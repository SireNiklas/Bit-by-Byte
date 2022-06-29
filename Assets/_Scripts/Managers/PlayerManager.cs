using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField]public GameObject _player;
    [SerializeField]public List<GameObject> PlayerSpawns;
    
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerSpawns.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn"));
        GameObject _Player = Instantiate(_player, PlayerSpawns[Random.Range(0, PlayerSpawns.Count)].transform.position, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
