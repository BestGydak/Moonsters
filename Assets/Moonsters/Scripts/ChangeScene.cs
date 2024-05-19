using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moonsters
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private SceneID scene;

        public void Change()
        {
            SceneManager.LoadScene((int)scene);
        }
    }

    public enum SceneID
    {
        MainMenu = 0,
        Game = 1,
        AstronautWin = 2,
        MonsterWin = 3
    }
}
