using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private Slider _Health;

    [SerializeField] private GameObject _enemyPrefab;

    private GameObject Enemy;

    private bool _isDead;

    // Start is called before the first frame update
    void Start()
    {

        _isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (_Health.value is 0)
        {
            _isDead = true;
            Destroy(gameObject);

        }
        EnemySpawn();

    }

    void HitByRay()
    {

        Debug.Log("Hit by ray");

        _Health.value -= 25;

    }

    private void Death()
    {

    }

    private void EnemySpawn()
    {

        if (_isDead == true)
        {
            //Enemy = Instantiate(_enemyPrefab, transform.position, transform.rotation);
            //_isDead = false;

        }

    }

}
