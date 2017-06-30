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
        public ColorBall ColorBall;

        public Vector2 TimeRange;

        private float _maxTime;
        private float _currentTime = 0;

        public float ColorRatio;
        public float ObstacleRatio;

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
            Transform currentPoint;

            if (CurrentIsUpside)
            {
                currentPoint = UpsidePoint;
            }
            else
            {
                currentPoint = DownsidePoint;
            }

            float randomValue = Random.Range(0, ColorRatio + ObstacleRatio);

            if (randomValue < ColorRatio)
            {
                PopColorBall(currentPoint);
            }
            else
            {
                PopObstacle(currentPoint);
            }
        }

        private void PopObstacle(Transform position)
        {
            Obstacle obstacle = Instantiate<Obstacle>(Obstacle);

            obstacle.transform.position = position.position;
            obstacle.transform.rotation = position.rotation;

            obstacle.SetSpeed(Speed);
        }

        private void PopColorBall(Transform position)
        {
            ColorBall colorBall = Instantiate<ColorBall>(ColorBall);

            colorBall.transform.position = position.position;
            colorBall.transform.rotation = position.rotation;

            colorBall.SetSpeed(Speed);

            int randomValue = Random.Range(0, 3);

            colorBall.SetColor(ColorBallManager.instance.ColorList[randomValue]);
        }

        private void SetMaxTime()
        {
            _maxTime = Random.Range(TimeRange.x, TimeRange.y);
            _currentTime = 0;
        }
    }
}
