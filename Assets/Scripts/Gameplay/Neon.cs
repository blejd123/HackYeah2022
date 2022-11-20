using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class Neon : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _minIntenisty;
        [SerializeField] private float _maxIntenisty;
        [SerializeField] private float _duration;
        
        private Material _material;
        private int _property;
        
        private void OnEnable()
        {
            _property = Shader.PropertyToID("_EmissionColor");
            _material = Instantiate(_renderer.sharedMaterial);
            _renderer.sharedMaterial = _material;

            DOVirtual.Float(_minIntenisty, _maxIntenisty, _duration, OnUpdate)
                .SetEase(Ease.InOutBounce).SetLoops(-1, LoopType.Yoyo).SetTarget(this);
        }

        private void OnDisable()
        {
            this.DOKill();
            
            if (_material != null)
            {
                Destroy(_material);
            }
        }

        private void OnUpdate(float value)
        {
            _material.SetColor(_property, Color.yellow * Mathf.LinearToGammaSpace(value));
        }
    }
}
