using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EpicGameJam
{  
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(AudioSource))]
    public class Obstacle : Movable
    {
        public Sprite DestroySprite;
        public float DisapearSpeed;

        public bool IsDisapearing = false;

        private SpriteRenderer _spriteRenderer;

        public AudioClip HitSound;

        private AudioSource _audioSource;

        protected override void Awake()
        { 
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();

            _audioSource = GetComponent<AudioSource>();
        }

        protected override void Update()
        {
            base.Update();

            if (IsDisapearing)
            {
                Color color = _spriteRenderer.color;

                color.a -= DisapearSpeed * Time.deltaTime;

                _spriteRenderer.color = color;
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player" && !IsDisapearing)
            {
                PlayerData.instance.Collision();

                _audioSource.PlayOneShot(HitSound);
                _spriteRenderer.sprite = DestroySprite;
                IsDisapearing = true;
            }

            else if (collider.tag == "EndLevel")
            {
                Destroy(this);
            }
        }
    }
}
