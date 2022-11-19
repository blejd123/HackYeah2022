using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    //[SerializeField] private

    public void AnimateDefault()
    {
        transform.DOKill();
        transform.DOLocalMoveY(transform.position.y + 0.75f, 0.5f).SetLoops(1, LoopType.Yoyo);
        //transform.DOShakeRotation(1.0f, new Vector3(1.0f, 0.5f, 1.0f)).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOLocalMoveY(transform.position.y + 0.75f, 0.5f).SetDelay(Random.Range(1.75f, 2.25f)).SetLoops(-1, LoopType.Yoyo);
    }
}
