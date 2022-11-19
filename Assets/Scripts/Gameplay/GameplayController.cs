using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ModestTree;
using UnityEngine;
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

        [SerializeField] private float _initialObstacleMoveDuration;
        [SerializeField] private float _obstacleMoveDurationChange;
        [SerializeField] private float _minObstacleMoveDurationChange;
        [SerializeField] private PlayableDirector _introTimeline;
        [SerializeField] private PlayableDirector _nextObstacleTimeline;
        [SerializeField] private PlayableDirector _loseTimeline;
        [SerializeField] private List<Obstacle> _obstacles;

        private void Start()
        {
            _introTimeline.Play();
        }

        public void StartObstaclesFlow()
        {
            StartCoroutine(StartObstaclesFlowCoroutine());
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
                yield return _obstacleTrack.AnimateObstacle(moveDuration).WaitForCompletion();
                moveDuration += _obstacleMoveDurationChange;
                moveDuration = Mathf.Clamp(moveDuration, _initialObstacleMoveDuration, _minObstacleMoveDurationChange);
                _obstacleTrack.DestroyObstacle();
                yield return _obstacleTrack.ShowCurtains().WaitForCompletion();
            }
        }
    }
}
