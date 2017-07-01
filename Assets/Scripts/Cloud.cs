using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cloud : MonoBehaviour
    {
        public float DisapearSpeed;

        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ResetCloud()
        {
            Color color = _spriteRenderer.color;

            color.a = 1f;

            _spriteRenderer.color = color;
        }

        void Update()
        {
            Color color = _spriteRenderer.color;

            color.a -= DisapearSpeed * Time.deltaTime;

            if (color.a <= 0)
            {
                gameObject.SetActive(false);
            }

            _spriteRenderer.color = color;
        }
    }
}
