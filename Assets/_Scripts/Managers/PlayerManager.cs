using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {


        //PlayerMovementController playerMovementController;

        [SerializeField] public GameObject _playerPrefab;
        [SerializeField] public List<GameObject> PlayerSpawns;

        //public float playerVelocity;

        //private void Awake()
        //{
            
        //}

        // Start is called before the first frame update
        void Start()
        {

            //playerVelocity = playerMovementController;


            //PlayerSpawns.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawn"));
            //GameObject _Player = Instantiate(_player, PlayerSpawns[Random.Range(0, PlayerSpawns.Count)].transform.position, transform.rotation);

            //GameObject _player = Instantiate(_player, transform.position, transform.rotation);
        }

        public bool EnablePlayerComponents(bool _isEnabled)
        {

            _playerPrefab = GameObject.FindGameObjectWithTag("Player");

            _playerPrefab.GetComponent<PlayerMovementController>().enabled = _isEnabled;
            _playerPrefab.GetComponent<PlayerStats>().enabled = _isEnabled;
            _playerPrefab.GetComponent<PlayerActionsController>().enabled = _isEnabled;
            _playerPrefab.GetComponent<CharacterCustomizationInitialization>().enabled = _isEnabled;

            //_playerHud.SetActive(true);

            return _isEnabled;

        }

        public void SpawnPlayer()
        {

            GameObject _player = Instantiate(_playerPrefab, transform.position, transform.rotation);

            _player.name = "Player";
            //_player.tag = "Player";

        }

    }
}
