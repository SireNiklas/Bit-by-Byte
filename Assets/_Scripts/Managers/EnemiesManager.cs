using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Enemies
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {

        //[SerializeField] private GameObject _enemyPrefab;

        [SerializeField] private int _enemyCount;

        // Start is called before the first frame update
        void Start()
        {

            //for (int i = 0; i < 5; i++)
            //{
            //    GameObject _enemy = Instantiate(_enemyPrefab, new Vector3(0, 1, 0), transform.rotation);
            //    gameObject.name = ("Enemy " + _enemyCount);
            //    _enemyCount += 1;
            //}

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
