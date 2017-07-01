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

        //public Vector2 TimeRange;

        //private float _maxTime;
        //private float _currentTime = 0;

        //public float ColorRatio;
        //public float ObstacleRatio;

        public Vector2 ColorSequenceRange;

        public Vector2 TimeObstacleSameSide;
        public float MinTimeObstacleOtherSide;
        public float TimeBetweenColors;

        private float _timeCountObstacleUp = 0;
        private float _timeCountObstacleDown = 0;

        //private float _timeSinceLastObstacleUp;
        //private float _timeSinceLastObstacleDown;

        private float _nextTimeObstacleUp;
        private float _nextTimeObstacleDown;

        private int _colorSequenceCount;
        private int _currentSequence;

        void Start()
        {
            _nextTimeObstacleDown = 1;
            _nextTimeObstacleUp = _nextTimeObstacleDown + MinTimeObstacleOtherSide;
        }

        void Update()
        {
            _timeCountObstacleUp += Time.deltaTime;
            _timeCountObstacleDown += Time.deltaTime;

            bool manageUpFirst = Random.value > 0.5f;

            if (manageUpFirst)
            {
                ManageObstacleUp();
                ManageObstacleDown();
            }
            else
            {
                ManageObstacleDown();
                ManageObstacleUp();
            }

            //_currentTime += Time.deltaTime;

            //if (_currentTime > _maxTime)
            //{
            //    SetMaxTime();
            //    Pop();
            //    CurrentIsUpside = !CurrentIsUpside;
            //}
        }

        private void ManageObstacleUp()
        {
            if (_timeCountObstacleUp > _nextTimeObstacleUp)
            {
                PopObstacle(UpsidePoint);

                _timeCountObstacleUp = 0;

                if (_timeCountObstacleDown + TimeObstacleSameSide.x > MinTimeObstacleOtherSide
                    && _nextTimeObstacleDown - _timeCountObstacleDown - TimeObstacleSameSide.x > MinTimeObstacleOtherSide)
                {
                    bool popBetween = Random.value > 0.5f;

                    if (popBetween)
                    {
                        _nextTimeObstacleUp = Random.Range(TimeObstacleSameSide.x, MinTimeObstacleOtherSide);
                    }
                    else
                    {
                        _nextTimeObstacleUp = Random.Range(_nextTimeObstacleDown - _timeCountObstacleDown + MinTimeObstacleOtherSide,
                                _nextTimeObstacleDown - _timeCountObstacleDown +
                                (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                    }
                }
                else
                {
                    _nextTimeObstacleUp = Random.Range(_nextTimeObstacleDown - _timeCountObstacleDown + MinTimeObstacleOtherSide,
                        _nextTimeObstacleDown - _timeCountObstacleDown +
                        (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                }
            }
        }

        private void ManageObstacleDown()
        {
            if (_timeCountObstacleDown > _nextTimeObstacleDown)
            {
                PopObstacle(DownsidePoint);
                _timeCountObstacleDown = 0;

                if (_timeCountObstacleUp + TimeObstacleSameSide.x > MinTimeObstacleOtherSide
                    && _nextTimeObstacleUp - _timeCountObstacleUp - TimeObstacleSameSide.x > MinTimeObstacleOtherSide)
                {
                    bool popBetween = Random.value > 0.5f;

                    if (popBetween)
                    {
                        _nextTimeObstacleDown = Random.Range(TimeObstacleSameSide.x, MinTimeObstacleOtherSide);
                    }
                    else
                    {
                        _nextTimeObstacleDown = Random.Range(_nextTimeObstacleUp - _timeCountObstacleUp + MinTimeObstacleOtherSide,
                                _nextTimeObstacleUp - _timeCountObstacleUp +
                                (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                    }
                }
                else
                {
                    _nextTimeObstacleDown = Random.Range(_nextTimeObstacleUp - _timeCountObstacleUp + MinTimeObstacleOtherSide,
                            _nextTimeObstacleUp - _timeCountObstacleUp +
                            (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                }
            }
        }

        //private void Pop()
        //{
        //    Transform currentPoint;

        //    if (CurrentIsUpside)
        //    {
        //        currentPoint = UpsidePoint;
        //    }
        //    else
        //    {
        //        currentPoint = DownsidePoint;
        //    }

        //    float randomValue = Random.Range(0, ColorRatio + ObstacleRatio);

        //    if (randomValue < ColorRatio)
        //    {
        //        PopColorBall(currentPoint);
        //    }
        //    else
        //    {
        //        PopObstacle(currentPoint);
        //    }
        //}

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

        //private void SetMaxTime()
        //{
        //    _maxTime = Random.Range(TimeRange.x, TimeRange.y);
        //    _currentTime = 0;
        //}
    }
}
