using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class ColorBall : Movable
    {
        private SpriteRenderer _spriteRender;
        public EColor ColorName;

        protected override void Awake()
        {
            base.Awake();

            _spriteRender = GetComponent<SpriteRenderer>();
        }

        public void SetColor(ColorEntry colorEntry)
        {
            _spriteRender.color = colorEntry.Color;
            ColorName = colorEntry.ColorName;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                PlayerData.instance.ColorScores[(int) ColorName] += 5;
                Destroy(this.gameObject);
            }
        }
    }
}
