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

        [SerializeField] private float _initialObstacleMoveDuration;
        [SerializeField] private float _obstacleMoveDurationChange;
        [SerializeField] private float _minObstacleMoveDurationChange;
        [SerializeField] private PlayableDirector _introTimeline;
        [SerializeField] private PlayableDirector _nextObstacleTimeline;
        [SerializeField] private PlayableDirector _loseTimeline;
        [SerializeField] private List<Obstacle> _obstacles;
        [SerializeField] private List<GiraffeController> _giraffes;

        private void Start()
        {
            _audience.Initialize();
            DisableInput();
            _introTimeline.Play();
        }

        private void Update()
        {
            if (Keyboard.current.f2Key.wasPressedThisFrame)
            {
                AddGiraffe();
            }
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

        private IEnumerator StartObstaclesFlowCoroutine()
        {
            _obstacleTrack.InitCurtains();

            var moveDuration = _initialObstacleMoveDuration;

            while (true)
            {
                var obstacle = _obstacles[Random.Range(0, _obstacles.Count)];
                _obstacleTrack.SetupObstacle(obstacle);
                yield return _obstacleTrack.HideCurtains().WaitForCompletion();
                EnableInput();
                yield return _obstacleTrack.AnimateObstacle(moveDuration).WaitForCompletion();
                moveDuration += _obstacleMoveDurationChange;
                moveDuration = Mathf.Clamp(moveDuration, _initialObstacleMoveDuration, _minObstacleMoveDurationChange);
                _obstacleTrack.DestroyObstacle();
                yield return _obstacleTrack.ShowCurtains().WaitForCompletion();
            }
        }

        private void AddGiraffe()
        {
            var giraffe = _giraffes[Random.Range(0, _giraffes.Count)];
            _giraffeFactory.Create(giraffe);
        }
    }
}
