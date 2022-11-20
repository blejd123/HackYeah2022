using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class Caster : MonoBehaviour
    {
        [SerializeField] private Transform _animationRoot;
    
        private void Start()
        {
            _animationRoot.DOKill();
            _animationRoot.DOShakeRotation(Random.Range(0.75f, 1.25f), new Vector3(1.0f, 0.0f, 1.0f) * 3, 2, 45.0f).SetLoops(-1).SetEase(Ease.Linear);
        }

        private void OnDestroy()
        {
            _animationRoot.DOKill();
        }
    }
}
