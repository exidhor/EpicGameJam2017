using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class Grass : Movable
    {
        public float AlphaValueEnter;

        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                Color color = _spriteRenderer.color;

                color.a = AlphaValueEnter;

                _spriteRenderer.color = color;
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                Color color = _spriteRenderer.color;

                color.a = 1f;

                _spriteRenderer.color = color;
            }
        }
    }
}
