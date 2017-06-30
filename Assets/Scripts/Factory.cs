using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EpicGameJam
{
    public class Factory : MonoSingleton<Factory>
    {
        public Transform UpsidePoint;
        public Transform DownsidePoint;

        public float Speed;

        public bool CurrentIsUpside = false;

        public Obstacle Obstacle;

        public Vector2 TimeRange;

        private float _maxTime;
        private float _currentTime = 0;

        void Start()
        {

        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _maxTime)
            {
                SetMaxTime();
                Pop();
                CurrentIsUpside = !CurrentIsUpside;
            }
        }

        private void Pop()
        {
            Obstacle obstacle = Instantiate<Obstacle>(Obstacle);

            Transform currentPoint;

            if (CurrentIsUpside)
            {
                currentPoint = UpsidePoint;
            }
            else
            {
                currentPoint = DownsidePoint;
            }

            obstacle.transform.position = currentPoint.position;
            obstacle.transform.rotation = currentPoint.rotation;

            obstacle.SetSpeed(Speed);
        }

        private void SetMaxTime()
        {
            _maxTime = Random.Range(TimeRange.x, TimeRange.y);
            _currentTime = 0;
        }
    }
}
