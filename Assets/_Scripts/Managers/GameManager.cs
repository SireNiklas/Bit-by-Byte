using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using UnityEngine.SceneManagement;
using Managers;

namespace Managers.Game {
    public class GameManager : Singleton<GameManager>
    {
        //public GameObject Player;
        [SerializeField] bool lockedCurser = true;

        //[SerializeField] private GameObject _sun;

        [SerializeField] private bool _isPaused = false;

        //private void Awake()
        //{

        //}

        // Start is called before the first frame update
        void Start()
        {

            //LockCurser();

            bool activeScene = Levels.LevelsManager.Instance.ActiveScene((int)Levels.LevelsIndex.Pre_Game);

            if (activeScene)
                SceneManager.LoadScene((int)Levels.LevelsIndex.HUBREALM);
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(KeyCode.Escape) && _isPaused == false)
            {

                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                _isPaused = true;

            } else if (Input.GetKey(KeyCode.Escape) && _isPaused == true)
            {

                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                _isPaused = false;
            }

        }

        public void LockCurser()
        {

            if (lockedCurser)
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

        private void GamepadCheck()
        {

            if (Gamepad.all.Count > 0)
                Debug.Log(Gamepad.current);
        }

    }
}
