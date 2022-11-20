using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
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
        [Inject] private readonly SoundController _soundController;

        [SerializeField] private float _initialObstacleMoveDuration;
        [SerializeField] private float _obstacleMoveDurationChange;
        [SerializeField] private float _minObstacleMoveDurationChange;
        [SerializeField] private PlayableDirector _introTimeline;
        [SerializeField] private PlayableDirector _nextObstacleTimeline;
        [SerializeField] private PlayableDirector _loseTimeline;
        [SerializeField] private List<Obstacle> _obstacles;
        [SerializeField] private List<GiraffeController> _giraffes;
        [SerializeField] private AudioClip _music;
        [SerializeField] private List<AudioClip> _casterSounds;

        private bool _obstacleHit;
        private GiraffeController _giraffeInstance;

        public ObstacleTrack ObstacleTrack => _obstacleTrack;

        public bool ObstacleHit => _obstacleHit;

        private void Start()
        {
            _soundController.PlayMusic(null, true);
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
                if (_giraffeInstance != null)
                {
                    _giraffeInstance.ResetNeck();
                }
                EnableInput();
                yield return _obstacleTrack.AnimateObstacle(moveDuration).WaitForCompletion();

                if (_obstacleHit)
                {
                    yield return _obstacleTrack.HidePitDoors().WaitForCompletion();
                    yield return _obstacleTrack.AnimateObstacleToPit().WaitForCompletion();
                    yield return _obstacleTrack.ShowPitDoors().WaitForCompletion();
                    moveDuration += _obstacleMoveDurationChange;
                    moveDuration = Mathf.Clamp(moveDuration, _minObstacleMoveDurationChange, _initialObstacleMoveDuration);
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
                    moveDuration = Mathf.Clamp(moveDuration, _minObstacleMoveDurationChange, _initialObstacleMoveDuration);
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

        public void PlayCasterSounds()
        {
            StartCoroutine(PlayCasterSoundsCorutine());
        }

        private IEnumerator PlayCasterSoundsCorutine()
        {
            var usedSounds = new List<AudioClip>();
            
            for (int i = 0; i < 1; i++)
            {
                while (true)
                {
                    var r = _casterSounds[Random.Range(0, _casterSounds.Count)];
                    if (!usedSounds.Contains(r))
                    {
                        usedSounds.Add(r);
                        _soundController.PlaySound(r);
                        yield return new WaitForSeconds(r.length);
                        break;
                    }
                }
            }
            
            PlayMusic();
        }

        private void PlayMusic()
        {
            _soundController.PlayMusic(_music, true);
        }
    }
}
