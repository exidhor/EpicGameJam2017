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
        public float MaxSpeed;

        public float Acceleration;

        //public bool CurrentIsUpside = false;

        public Obstacle Obstacle;
        public ColorBall ColorBall;
        public Grass Grass;

        //public Vector2 TimeRange;

        //private float _maxTime;
        //private float _currentTime = 0;

        //public float ColorRatio;
        //public float ObstacleRatio;

        public Vector2 ColorSequenceRange;

        public Vector2 TimeObstacleSameSide;
        public float MinTimeObstacleOtherSide;
        public float TimeBetweenColors;
        public float TimeBetweenObstacleAndColor;

        public float MaxDeltaColor;
        public float MinColorValueForConstraint;

        public Vector2 TimeBetweenGrass;

        private float _timeCountUp = 0;
        private float _timeCountDown = 0;

        private float _nextTimeObstacleUp;
        private float _nextTimeObstacleDown;

        private int _colorSequenceCountUp;
        private int _colorSequenceCountDown;

        private int _currentSequenceIndexUp;
        private int _currentSequenceIndexDown;

        private float _nextGrassTime;
        private float _currentGrassTime;
        private bool _nextGrassIsUpside;

        private float _startSpeed;

        void Start()
        {
            _nextTimeObstacleDown = 1;
            _nextTimeObstacleUp = _nextTimeObstacleDown + MinTimeObstacleOtherSide;

            _startSpeed = Speed;
        }

        public void DecreaseSpeedOnCollision()
        {
            Speed /= 2;

            if (Speed < _startSpeed)
            {
                Speed = _startSpeed;
            }
        }

        void Update()
        {
            _timeCountUp += Time.deltaTime;
            _timeCountDown += Time.deltaTime;

            bool manageUpFirst = Random.value > 0.5f;

            if (manageUpFirst)
            {
                ManageUp();
                ManageDown();
            }
            else
            {
                ManageDown();
                ManageUp();
            }

            ManageGrass();

            //_currentTime += Time.deltaTime;

            //if (_currentTime > _maxTime)
            //{
            //    SetMaxTime();
            //    Pop();
            //    CurrentIsUpside = !CurrentIsUpside;
            //}
        }

        void FixedUpdate()
        {
            Speed += Acceleration*Time.fixedDeltaTime;

            if (Speed > MaxSpeed)
            {
                Speed = MaxSpeed;
            }
        }

        private void ManageUp()
        {
            ManageObstacleUp();
            ManageColorUp();
        }

        private void ManageDown()
        {
            ManageObstacleDown();
            ManageColorDown();
        }

        private void ManageColorUp()
        {
            if (_currentSequenceIndexUp < _colorSequenceCountUp
                && _timeCountUp > TimeBetweenObstacleAndColor + _currentSequenceIndexUp*TimeBetweenColors)
            {
                int randomValue;

                if (_currentSequenceIndexUp == 0)
                {
                    randomValue = GetColorIndexWithConstraint();
                }
                else
                {
                    randomValue = Random.Range(0, 3);
                }

                PopColorBall(UpsidePoint, randomValue);
                _currentSequenceIndexUp++;
            }
        }

        private void ManageColorDown()
        {
            if (_currentSequenceIndexDown < _colorSequenceCountDown
                && _timeCountDown > TimeBetweenObstacleAndColor + _currentSequenceIndexDown * TimeBetweenColors)
            {
                int randomValue;

                if (_currentSequenceIndexDown == 0)
                {
                    randomValue = GetColorIndexWithConstraint();
                }
                else
                {
                    randomValue = Random.Range(0, 3);
                }

                PopColorBall(DownsidePoint, randomValue);
                _currentSequenceIndexDown++;
            }
        }

        private void ManageGrass()
        {
            _currentGrassTime += Time.deltaTime;

            float maxColorValue = float.MinValue;

            for (int i = 0; i < 3; i++)
            {
                if (maxColorValue < PlayerData.instance.ColorScores[i])
                {
                    maxColorValue = PlayerData.instance.ColorScores[i];
                }
            }

            if (maxColorValue > 50)
            {
                if (_currentGrassTime > _nextGrassTime)
                {
                    _currentGrassTime = 0;

                    _nextGrassTime = Random.Range(TimeBetweenGrass.x, TimeBetweenGrass.y);

                    PopGrass();
                }
            }
        }

        private void PopGrass()
        {
            Grass grass = Instantiate<Grass>(Grass);

            if (_nextGrassIsUpside)
            {
                grass.transform.position = UpsidePoint.position;
                grass.transform.rotation = UpsidePoint.rotation;
            }
            else
            {
                grass.transform.position = DownsidePoint.position;
                grass.transform.rotation = DownsidePoint.rotation;
            }

            _nextGrassIsUpside = Random.value > 0.5f;
        }

        void SetColorTimeUp()
        {
            _currentSequenceIndexUp = 0;

            float distanceBetweenObstacle = _nextTimeObstacleUp - 2*TimeBetweenObstacleAndColor;

            if (distanceBetweenObstacle > 0)
            {
                int colorMax = (int) ((distanceBetweenObstacle)/TimeBetweenColors) + 1;

                if (colorMax >= ColorSequenceRange.x)
                {
                    int maxGeneratedColor = (int) Mathf.Min(colorMax, ColorSequenceRange.y);

                    _colorSequenceCountUp = Random.Range((int) ColorSequenceRange.x, maxGeneratedColor);
                }
            }
            else
            {
                _colorSequenceCountUp = 0;
            }
        }

        void SetColorTimeDown()
        {
            _currentSequenceIndexDown = 0;

            float distanceBetweenObstacle = _nextTimeObstacleDown - 2 * TimeBetweenObstacleAndColor;

            if (distanceBetweenObstacle > 0)
            {
                int colorMax = (int)((distanceBetweenObstacle) / TimeBetweenColors);

                if (colorMax >= ColorSequenceRange.x)
                {
                    int maxGeneratedColor = (int)Mathf.Min(colorMax, ColorSequenceRange.y);

                    _colorSequenceCountDown = Random.Range((int)ColorSequenceRange.x, maxGeneratedColor);
                }
            }
            else
            {
                _colorSequenceCountDown = 0;
            }
        }

        private void ManageObstacleUp()
        {
            if (_timeCountUp > _nextTimeObstacleUp)
            {
                PopObstacle(UpsidePoint);

                _timeCountUp = 0;

                if (_timeCountDown + TimeObstacleSameSide.x > MinTimeObstacleOtherSide
                    && _nextTimeObstacleDown - _timeCountDown - TimeObstacleSameSide.x > MinTimeObstacleOtherSide)
                {
                    bool popBetween = Random.value > 0.5f;

                    if (popBetween)
                    {
                        _nextTimeObstacleUp = Random.Range(TimeObstacleSameSide.x, MinTimeObstacleOtherSide);
                    }
                    else
                    {
                        _nextTimeObstacleUp =
                            Random.Range(_nextTimeObstacleDown - _timeCountDown + MinTimeObstacleOtherSide,
                                _nextTimeObstacleDown - _timeCountDown +
                                (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                    }
                }
                else
                {
                    _nextTimeObstacleUp = Random.Range(
                        _nextTimeObstacleDown - _timeCountDown + MinTimeObstacleOtherSide,
                        _nextTimeObstacleDown - _timeCountDown +
                        (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                }

                SetColorTimeUp();
            }
        }

        private void ManageObstacleDown()
        {
            if (_timeCountDown > _nextTimeObstacleDown)
            {
                PopObstacle(DownsidePoint);
                _timeCountDown = 0;

                if (_timeCountUp + TimeObstacleSameSide.x > MinTimeObstacleOtherSide
                    && _nextTimeObstacleUp - _timeCountUp - TimeObstacleSameSide.x > MinTimeObstacleOtherSide)
                {
                    bool popBetween = Random.value > 0.5f;

                    if (popBetween)
                    {
                        _nextTimeObstacleDown = Random.Range(TimeObstacleSameSide.x, MinTimeObstacleOtherSide);
                    }
                    else
                    {
                        _nextTimeObstacleDown =
                            Random.Range(_nextTimeObstacleUp - _timeCountUp + MinTimeObstacleOtherSide,
                                _nextTimeObstacleUp - _timeCountUp +
                                (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                    }
                }
                else
                {
                    _nextTimeObstacleDown = Random.Range(_nextTimeObstacleUp - _timeCountUp + MinTimeObstacleOtherSide,
                        _nextTimeObstacleUp - _timeCountUp +
                        (TimeObstacleSameSide.y - TimeObstacleSameSide.x));
                }

                SetColorTimeDown();
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

            //obstacle.SetSpeed(Speed);
        }

        private void PopColorBall(Transform position, int index)
        {
            ColorBall colorBall = Instantiate<ColorBall>(ColorBall);

            colorBall.transform.position = position.position;
            colorBall.transform.rotation = position.rotation;

            //colorBall.SetSpeed(Speed);


            colorBall.SetColor(ColorBallManager.instance.ColorList[index]);
        }

        //private void SetMaxTime()
        //{
        //    _maxTime = Random.Range(TimeRange.x, TimeRange.y);
        //    _currentTime = 0;
        //}

        private int GetColorIndexWithConstraint()
        {
            int smallestIndex = 0;

            for (int i = 1; i < 3; i++)
            {
                if (PlayerData.instance.ColorScores[i] < PlayerData.instance.ColorScores[smallestIndex])
                {
                    smallestIndex = i;
                }
            }


            if (MinColorValueForConstraint < PlayerData.instance.ColorScores[smallestIndex])
            {
                return smallestIndex;
            }

            int otherSmallestIndex = 0;

            for (int i = 1; i < 3; i++)
            {
                if (i != smallestIndex
                    && PlayerData.instance.ColorScores[i] < PlayerData.instance.ColorScores[otherSmallestIndex])
                {
                    otherSmallestIndex = i;
                }
            }

            float delta =
                Mathf.Abs(PlayerData.instance.ColorScores[smallestIndex] -
                          PlayerData.instance.ColorScores[otherSmallestIndex]);

            if (MaxDeltaColor < delta)
            {
                return smallestIndex;
            }

            return Random.Range(0, 3);
        }
    }
}