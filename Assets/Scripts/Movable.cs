using UnityEngine;

namespace EpicGameJam
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movable : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public float Speed;

        private static readonly float MAX_DISTANCE = -14f;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;

            _rb.velocity = new Vector2(-Speed, 0);
        }

        protected virtual void Update()
        {
            if (transform.position.x < MAX_DISTANCE)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
