using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [Inject] private readonly CameraController _cameraController;
        [Inject] private readonly Podium _podium;
        [Inject] private readonly ObstacleTrack _obstacleTrack;
        [Inject] private readonly InputController _inputController;
        [Inject] private readonly GiraffeController.Factory _giraffeFactory;
        [Inject] private readonly Audience _audience;
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private float _initialObstacleMoveDuration;
        [SerializeField] private float _obstacleMoveDurationChange;
        [SerializeField] private float _minObstacleMoveDurationChange;
        [SerializeField] private PlayableDirector _introTimeline;
        [SerializeField] private PlayableDirector _nextObstacleTimeline;
        [SerializeField] private PlayableDirector _loseTimeline;
        [SerializeField] private List<Obstacle> _obstacles;
        [SerializeField] private List<GiraffeController> _giraffes;

        private bool _obstacleHit;
        private GiraffeController _giraffeInstance;

        public ObstacleTrack ObstacleTrack => _obstacleTrack;

        public bool ObstacleHit => _obstacleHit;

        private void Start()
        {
            _audience.Initialize();
            Initialize();
            _introTimeline.Play();
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ObstacleHitGiraffeSignal>(OnObstacleHit);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ObstacleHitGiraffeSignal>(OnObstacleHit);
        }

        private void Initialize()
        {
            var giraffe = _giraffes[Random.Range(0, _giraffes.Count)];
            _giraffeInstance = _giraffeFactory.Create(giraffe);
            _giraffeInstance.transform.SetParent(_podium.Positions[0]);
            _giraffeInstance.transform.localPosition = Vector3.zero;
            DisableInput();
        }
        
        public void StartObstaclesFlow()
        {
            StartCoroutine(StartObstaclesFlowCoroutine());
        }

        public void EnableInput()
        {
            _inputController.enabled = true;
        }

        public void DisableInput()
        {
            if (_inputController.enabled)
            {
                _inputController.enabled = false;
            }
        }

        public void ObstacleAvoided()
        {
            _audience.Jump();
        }

        private IEnumerator StartObstaclesFlowCoroutine()
        {
            _obstacleTrack.InitCurtains();
            _obstacleTrack.InitPitDoors();

            var moveDuration = _initialObstacleMoveDuration;

            while (true)
            {
                var obstacle = _obstacles[Random.Range(0, _obstacles.Count)];
                _obstacleTrack.SetupObstacle(obstacle);
                _obstacleHit = false;
                yield return _obstacleTrack.HideCurtains().WaitForCompletion();
                EnableInput();
                yield return _obstacleTrack.AnimateObstacle(moveDuration).WaitForCompletion();

                if (_obstacleHit)
                {
                    yield return _obstacleTrack.HidePitDoors().WaitForCompletion();
                    yield return _obstacleTrack.AnimateObstacleToPit().WaitForCompletion();
                    yield return _obstacleTrack.ShowPitDoors().WaitForCompletion();
                    moveDuration += _obstacleMoveDurationChange;
                    moveDuration = Mathf.Clamp(moveDuration, _initialObstacleMoveDuration, _minObstacleMoveDurationChange);
                    _obstacleTrack.DestroyObstacle();
                    yield return _obstacleTrack.ShowCurtains().WaitForCompletion();

                    if (_giraffeInstance != null)
                    {
                        Destroy(_giraffeInstance.gameObject);
                        _giraffeInstance = null;
                    }
                    
                    _loseTimeline.Stop();
                    _nextObstacleTimeline.Play();
                    yield return null;
                    Initialize();
                }
                else
                {
                    moveDuration += _obstacleMoveDurationChange;
                    moveDuration = Mathf.Clamp(moveDuration, _initialObstacleMoveDuration, _minObstacleMoveDurationChange);
                    _obstacleTrack.DestroyObstacle();
                    yield return _obstacleTrack.ShowCurtains().WaitForCompletion();   
                }
            }
        }
        
        private void OnObstacleHit()
        {
            _obstacleHit = true;
            _introTimeline.Stop();
            _nextObstacleTimeline.Stop();
            _loseTimeline.Play();
        }
    }
}
