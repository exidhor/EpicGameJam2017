using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movable : MonoBehaviour
    {
        protected Rigidbody2D _rb;
        public float Speed;

        public float TargetSpeed;

        protected GameObject _target = null;

        private static readonly float MAX_DISTANCE = -20f;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        //public void SetSpeed(float speed)
        //{
        //    Speed = speed;

        //    _rb.velocity = new Vector2(-Speed, 0);
        //}

        protected virtual void Update()
        {
            if (transform.position.x < MAX_DISTANCE)
            {
                Destroy(this.gameObject);
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_target != null)
            {
                Vector2 direction = _target.transform.position - transform.position;

                direction.Normalize();

                if (direction.magnitude > 0.01f)
                {
                    direction *= TargetSpeed;

                    _rb.velocity = direction;
                }
            }
            else
            {
                _rb.velocity = new Vector2(-Factory.instance.Speed, 0);
            }
        }

        public void SetTarget(GameObject go)
        {
            _target = go;
        }
    }
}
