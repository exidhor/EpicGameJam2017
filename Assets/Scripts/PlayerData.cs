using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;

namespace EpicGameJam
{
    public class PlayerData : MonoSingleton<PlayerData>
    {
        public float DecreaseValue;

        public float StartValue = 10f;

        public float CollisionCost;

        public List<float> ColorScores = new List<float>();

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
                
                GameManager.instance.EndGame();
            }
            else if(ColorScores[index] > 100)
            {
                ColorScores[index] = 100;
                Debug.Log("Victory");
            }
        }
    }
}
