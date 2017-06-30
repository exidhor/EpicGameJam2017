using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;

namespace EpicGameJam
{
    public class ColorBallManager : MonoSingleton<ColorBallManager>
    {
        public List<ColorEntry> ColorList = new List<ColorEntry>();

        public Color GetColor(EColor colorName)
        {
            return ColorList[(int) colorName].Color;
        }
    }
}
