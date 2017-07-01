using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EpicGameJam
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public GameObject PanelGameOver;

        public bool GameIsRunning = true;

        public void EndGame()
        {
            if (GameIsRunning)
            {
                GameIsRunning = false;
                Time.timeScale = 0;

                DisplayEndScreen();
            }
        }

        public void DisplayEndScreen()
        {
            PanelGameOver.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}