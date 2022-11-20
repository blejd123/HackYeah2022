using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration;

        private void Awake()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.0f;
        }

        public void FadeInImmediately()
        {
            _canvasGroup.DOKill();
            _canvasGroup.alpha = 1.0f;
        }
        
        public Tween FadeIn()
        {
            _canvasGroup.DOKill();
            return _canvasGroup.DOFade(1.0f, _fadeDuration).SetEase(Ease.Linear);
        }
        
        public Tween FadeOut()
        {
            _canvasGroup.DOKill();
            return _canvasGroup.DOFade(0.0f, _fadeDuration).SetEase(Ease.Linear);
        }
    }
}
