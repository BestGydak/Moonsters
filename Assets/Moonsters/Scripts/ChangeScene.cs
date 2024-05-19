using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moonsters
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private Scene scene;

        public void Change()
        {
            SceneManager.LoadScene((int)scene);
        }
    }

    public enum Scene
    {
        MainMenu = 0,
        Game = 1
    }
}
