using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;

namespace EpicGameJam
{
    public class PaintRollerManager : MonoSingleton<PaintRollerManager>
    {
        [SerializeField]
        private List<PaintRoller> _paintRollers = new List<PaintRoller>();

        void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                _paintRollers[i].SetColor(ColorBallManager.instance.ColorList[i].Color);
            }
        }

        public void UpdateColorValue(int index, float value)
        {
            _paintRollers[index].SetSize(value);
        }
    }
}
