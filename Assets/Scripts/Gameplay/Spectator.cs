using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField] private Transform _animationRoot;
    
    public void AnimateDefault()
    {
        _animationRoot.DOKill();
        _animationRoot.DOShakeRotation(Random.Range(0.75f, 1.25f), new Vector3(1.0f, 0.0f, 1.0f) * 3, 2, 45.0f).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void Jump()
    {
        _animationRoot.DOLocalJump(_animationRoot.position, 0.5f, Random.Range(1, 3), 1.0f).SetDelay(Random.Range(0.0f, 0.15f));
    }
}
