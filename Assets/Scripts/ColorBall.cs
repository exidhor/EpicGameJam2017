using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class ColorBall : Movable
    {
        private SpriteRenderer _spriteRender;
        public EColor ColorName;

        public bool StartDisapear;
        public float DisapearSpeed;

        public float TargetDistance;

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
                PlayerData.instance.AddColorScore((int) ColorName, 5);

                _target = PaintRollerManager.instance.GetPaintRoller(ColorName).gameObject;
            }
        }

        void Disapear()
        {
            transform.SetParent(_target.transform, true);

            _target = null;

            StartDisapear = true;
        }

        protected override void Update()
        {
            base.Update();

            if (StartDisapear)
            {
                Vector3 scale = transform.localScale;

                float decreaseScaleValue = DisapearSpeed*Time.deltaTime;

                scale.x -= decreaseScaleValue;
                scale.y -= decreaseScaleValue;

                if (scale.x <= 0)
                {
                    Destroy(this.gameObject);
                }

                transform.localScale = scale;
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_target != null)
            {
                float distance = Vector2.Distance(_target.transform.position, transform.position);

                if (TargetDistance > distance)
                {
                    PaintRoller paintRoller = _target.GetComponent<PaintRoller>();
                    PlayerData.instance.AddColorScore((int)paintRoller.ColorName, 5);

                    _rb.velocity = Vector2.zero;

                    Disapear();
                }
            }
        }
    }
}