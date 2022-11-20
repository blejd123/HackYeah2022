using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public sealed class GiraffeNeck : MonoBehaviour
{
    public bool IsResetInProgress { get; private set; }

    [SerializeField] private int _AngleStep = 15;
    [SerializeField] private List<GiraffeNeckElement> _NeckElements;

    [Inject] private SignalBus _SignalBus;

    private int _CurrentElementIndex;

    public IEnumerator ResetNeck()
    {
        IsResetInProgress = true;
        foreach(var neckElement in _NeckElements)
        {
            neckElement.ResetRotation(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        IsResetInProgress = false;
    }

    public void MoveUp()
    {
        _NeckElements[_CurrentElementIndex].Deselect();
        _CurrentElementIndex = Mathf.Clamp(_CurrentElementIndex + 1, 0, _NeckElements.Count - 1);
        _NeckElements[_CurrentElementIndex].Select();
    }

    public void MoveDown()
    {
        _NeckElements[_CurrentElementIndex].Deselect();
        _CurrentElementIndex = Mathf.Clamp(_CurrentElementIndex - 1, 0, _NeckElements.Count - 1);
        _NeckElements[_CurrentElementIndex].Select();
    }

    public void RotateLeft()
    {
        if (IsResetInProgress)
        {
            return;
        }

        _NeckElements[_CurrentElementIndex].RotateBone(-_AngleStep);
    }

    public void RotateRight()
    {
        if (IsResetInProgress)
        {
            return;
        }

        _NeckElements[_CurrentElementIndex].RotateBone(_AngleStep);
    }

    private void Start()
    {
        if (_NeckElements.Count == 0)
        {
            _NeckElements = GetComponentsInChildren<GiraffeNeckElement>().ToList();
            _NeckElements[_CurrentElementIndex].Select();
        }
    }

    private void OnEnable()
    {
        _SignalBus.Subscribe<ObstacleHitGiraffeSignal>(OnHit);
    }

    private void OnDisable()
    {
        _SignalBus.Unsubscribe<ObstacleHitGiraffeSignal>(OnHit);
    }

    private void OnHit()
    {
        _NeckElements[_CurrentElementIndex].Deselect();
    }
}