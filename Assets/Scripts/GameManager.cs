using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EpicGameJam
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public GameObject PanelGameOver;
        //public GameObject PanelVictory;

        public Text Title;
        public Text Score;
        public Text ColorCollected;

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
            Title.text = "GAME OVER";
            Score.text = "Score : " + (int) PlayerData.instance.Score;
            ColorCollected.text = "Color Collected : " + (int) PlayerData.instance.ColorCollected;

            PanelGameOver.SetActive(true);
        }

        public void DisplayVictoryScreen()
        {
            Title.text = "VICTORY";
            Score.text = "Score : " + (int)PlayerData.instance.Score;
            ColorCollected.text = "Color Collected : " + (int)PlayerData.instance.ColorCollected;

            PanelGameOver.SetActive(true);
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
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}