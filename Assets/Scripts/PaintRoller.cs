using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PaintRoller : MonoBehaviour
    {
        [SerializeField]
        private Color _traceColor;

        private SpriteRenderer _spriteRenderer;

        public List<Sprite> _animation;
        public float speedAnimation;

        private float _currentTime;
        private int _currentIndex;

        public SpriteRenderer Traced;
        public float Offset_x;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetColor(Color color)
        {
            Traced.color = color;
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > speedAnimation)
            {
                _currentTime = 0;
                _currentIndex++;

                if (_currentIndex >= _animation.Count)
                {
                    _currentIndex = 0;
                }

                _spriteRenderer.sprite = _animation[_currentIndex];
            }
        }

        public void SetSize(float size)
        {
            Vector3 scale = Traced.transform.localScale;

            scale.x = size;

            Traced.transform.localScale = scale;

            transform.position = Traced.bounds.center + new Vector3(Traced.bounds.extents.x + Offset_x, 0, 0);
        }
    }
}
