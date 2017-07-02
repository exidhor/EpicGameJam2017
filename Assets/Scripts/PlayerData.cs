using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace EpicGameJam
{
    public class PlayerData : MonoSingleton<PlayerData>
    {
        public float DecreaseValue;

        public float StartValue = 10f;

        public float CollisionCost;

        public List<float> ColorScores = new List<float>();

        public Text ScoreText;

        public float Score = 0;

        void Awake()
        {
            for (int i = 0; i < 3; i++)
            {
                ColorScores.Add(StartValue);
            }
        }

        void Update()
        {
            for (int i = 0; i < 3; i++)
            {
                AddColorScore(i, -DecreaseValue * Time.deltaTime);

                PaintRollerManager.instance.UpdateColorValue(i, ColorScores[i]);
            }

            Score += (Factory.instance.Speed + ColorScores[0] + ColorScores[1] + ColorScores[2])/10*Time.deltaTime;

            ScoreText.text = "Score : " + (int) Score;
        }

        public void Collision()
        {
            for (int i = 0; i < 3; i++)
            {
                AddColorScore(i, -CollisionCost);
            }

            Factory.instance.DecreaseSpeedOnCollision();
        }

        public void AddColorScore(int index, float add)
        {
            ColorScores[index] += add;

            if (ColorScores[index] < 0)
            {
                ColorScores[index] = 0;
                
                GameManager.instance.EndGame(true);
            }
            else if(ColorScores[index] > 100)
            {
                ColorScores[index] = 100;

                GameManager.instance.EndGame(false);
            }
        }
    }
}
