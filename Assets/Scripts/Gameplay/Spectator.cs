using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField] private Transform _animationRoot;
    
    public void AnimateDefault()
    {
        this.DOKill();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(0.75f, 1.0f));
        sequence.Append(transform.DOLocalMoveY(0.0f, 1.0f));
        sequence.SetLoops(-1);
        sequence.SetTarget(this);
        //transform.DOLocalMoveY(transform.position.y + 0.75f, 0.5f).SetLoops(1, LoopType.Yoyo);
        //transform.DOShakeRotation(1.0f, new Vector3(1.0f, 0.5f, 1.0f)).SetLoops(-1).SetEase(Ease.Linear);
        //transform.DOLocalMoveY(transform.position.y + 0.75f, 0.5f).SetDelay(Random.Range(1.75f, 2.25f)).SetLoops(-1, LoopType.Yoyo);
    }
}
