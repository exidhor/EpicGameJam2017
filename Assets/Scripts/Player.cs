using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public List<Sprite> _animation;
        public float speedAnimation;

        private float _currentTime = 0;
        private int _currentIndex;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}
