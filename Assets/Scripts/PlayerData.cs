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
                ColorScores[i] -= DecreaseValue*Time.deltaTime;

                PaintRollerManager.instance.UpdateColorValue(i, ColorScores[i]);
            }
        }
    }
}
