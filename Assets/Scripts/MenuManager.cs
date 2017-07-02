using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EpicGameJam
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject CreditPage;
        public GameObject Home;
        public GameObject RulesPage;

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void DisplayCreditsPage()
        {
            Home.SetActive(false);
            RulesPage.SetActive(false);
            CreditPage.SetActive(true);
        }

        public void DisplayRulesPage()
        {
            Home.SetActive(false);
            CreditPage.SetActive(false);
            RulesPage.SetActive(true);
        }

        public void DisplayHome()
        {
            CreditPage.SetActive(false);
            RulesPage.SetActive(false);
            Home.SetActive(true);
        }
    }
}
