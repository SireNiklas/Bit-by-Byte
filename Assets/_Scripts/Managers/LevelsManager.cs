using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Managers.Levels
{
    public class LevelsManager : Singleton<LevelsManager>
    {

        //bool Pre_Game = false;
        //bool Main_Game = false;
        //bool Post_Game = false;

        //private void Awake()
        //{
        //}

        public bool ActiveScene(int sceneName)
        {

            if (SceneManager.GetActiveScene().buildIndex == sceneName)
            {
                return true;

            }
            else { return false; }

        }

        public void LoadScene(int sceneName)
        {
            SceneManager.LoadScene(sceneName);

        }
    }

    public enum LevelsIndex
    {

        Pre_Game = 0,
        HUBREALM = 1,
        Post_Game = 2

    }
}