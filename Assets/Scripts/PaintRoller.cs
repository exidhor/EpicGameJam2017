using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{
    public class PaintRoller : MonoBehaviour
    {
        [SerializeField]
        private Color _traceColor;

        public SpriteRenderer Traced;

        public void SetColor(Color color)
        {
            Traced.color = color;
        }

        public void SetSize(float size)
        {
            Vector3 scale = Traced.transform.localScale;

            scale.x = size;

            Traced.transform.localScale = scale;

            transform.position = Traced.bounds.center + new Vector3(Traced.bounds.extents.x, 0, 0);
        }
    }
}
