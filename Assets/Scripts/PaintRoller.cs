using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EpicGameJam
{
    [RequireComponent(typeof(Collider2D))]
    public class PaintRoller : MonoBehaviour
    {
        public EColor ColorName;
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

            _currentTime = Random.Range(0, speedAnimation);
        }

        public void SetColor(ColorEntry colorEntry)
        {
            ColorName = colorEntry.ColorName;
            Traced.color = colorEntry.Color;
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > speedAnimation)
            {
                _currentTime -= speedAnimation;
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
