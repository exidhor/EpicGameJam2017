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
        public GameObject PanelVictory;

        public bool GameIsRunning = true;

        public void EndGame(bool IsGameOver)
        {
            if (IsGameOver)
            {
                if (GameIsRunning)
                {
                    GameIsRunning = false;
                    Time.timeScale = 0;

                    DisplayEndScreen();
                }
            }
            else
            {
                if (GameIsRunning)
                {
                    GameIsRunning = false;
                    Time.timeScale = 0;

                    DisplayVictoryScreen();
                }
            }
        }

        public void DisplayEndScreen()
        {
            PanelGameOver.SetActive(true);
        }

        public void DisplayVictoryScreen()
        {
            PanelVictory.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(1);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}