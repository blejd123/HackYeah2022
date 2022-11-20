using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ObstacleTrack : MonoBehaviour
    {
        [Inject] private GameplayController _gameplayController;
        
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private Transform _leftCurtain;
        [SerializeField] private Transform _rightCurtain;
        [SerializeField] private float _curtainMoveDuration;
        [SerializeField] private float _curtainMoveOffset;
        [SerializeField] private Transform _inputDisableLine;
        [SerializeField] private Transform _obstacleAvoidedLine;
        [SerializeField] private Renderer _outlineRenderer;
        [SerializeField] private Transform _leftPitDoor;
        [SerializeField] private Transform _rightPitDoor;
        [SerializeField] private float _pitDoorMoveDuration;
        [SerializeField] private float _pitDoorMoveOffset;

        [Inject] private Obstacle.Factory _ObstacleFactory;

        private Obstacle _currentObstacle;
        private Vector3 _leftCurtainDefaultPosition;
        private Vector3 _rightCurtainDefaultPosition;
        private Vector3 _leftPitDoorDefaultPosition;
        private Vector3 _rightPitDoorDefaultPosition;
        private bool _inputDisableSend;
        private bool _obstacleAvoided;

        private void Awake()
        {
            _leftCurtainDefaultPosition = _leftCurtain.transform.position;
            _rightCurtainDefaultPosition = _rightCurtain.transform.position;
            _leftPitDoorDefaultPosition = _leftPitDoor.transform.position;
            _rightPitDoorDefaultPosition = _rightPitDoor.transform.position;
        }

        public void InitCurtains()
        {
            _leftCurtain.position = _leftCurtainDefaultPosition;
            _rightCurtain.position = _rightCurtainDefaultPosition;
            _outlineRenderer.gameObject.SetActive(false);
        }
        
        public void InitPitDoors()
        {
            _leftPitDoor.position = _leftPitDoorDefaultPosition;
            _rightPitDoor.position = _rightPitDoorDefaultPosition;
        }

        public Tween ShowCurtains()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_leftCurtain.DOMoveX(_leftCurtainDefaultPosition.x, _curtainMoveDuration));
            sequence.Join(_rightCurtain.DOMoveX(_rightCurtainDefaultPosition.x, _curtainMoveDuration));
            return sequence;
        }

        public Tween HideCurtains()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_leftCurtain.DOMoveX(_leftCurtainDefaultPosition.x - _curtainMoveOffset, _curtainMoveDuration));
            sequence.Join(_rightCurtain.DOMoveX(_rightCurtainDefaultPosition.x + _curtainMoveOffset, _curtainMoveDuration));
            return sequence;
        }

        public Tween ShowPitDoors()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_leftPitDoor.DOMoveX(_leftPitDoorDefaultPosition.x, _pitDoorMoveDuration));
            sequence.Join(_rightPitDoor.DOMoveX(_rightPitDoorDefaultPosition.x, _pitDoorMoveDuration));
            return sequence;
        }

        public Tween HidePitDoors()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_leftPitDoor.DOMoveX(_leftPitDoorDefaultPosition.x - _pitDoorMoveOffset, _pitDoorMoveDuration));
            sequence.Join(_rightPitDoor.DOMoveX(_rightPitDoorDefaultPosition.x + _pitDoorMoveOffset, _pitDoorMoveDuration));
            return sequence;
        }
        
        public void SetupObstacle(Obstacle obstaclePrefab)
        {
            _currentObstacle = _ObstacleFactory.Create(obstaclePrefab);
            _currentObstacle.transform.position = _start.position;
            _inputDisableSend = false;
            _obstacleAvoided = false;

            _outlineRenderer.gameObject.SetActive(false);
            _outlineRenderer.sharedMaterial.mainTexture = _currentObstacle.Outline;
        }

        public void DestroyObstacle()
        {
            if (_currentObstacle != null)
            {
                Destroy(_currentObstacle.gameObject);
                _currentObstacle = null;
                _outlineRenderer.gameObject.SetActive(false);
            }
        }

        public Tween AnimateObstacle(float duration)
        {
            _outlineRenderer.gameObject.SetActive(true);
            return _currentObstacle.transform.DOMove(_end.position, duration).SetEase(Ease.Linear).OnUpdate(CheckObstaclePosition);
        }

        private void CheckObstaclePosition()
        {
            var pos = _currentObstacle.transform.position.z;
            
            if (_inputDisableSend == false && pos <= _inputDisableLine.position.z)
            {
                _inputDisableSend = true;
                _gameplayController.DisableInput();
                _outlineRenderer.gameObject.SetActive(false);
            }
            
            if (_obstacleAvoided == false && pos <= _obstacleAvoidedLine.position.z)
            {
                _obstacleAvoided = true;
                _gameplayController.ObstacleAvoided();
                _outlineRenderer.gameObject.SetActive(false);
            }
        }
    }
}
