using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GiraffeNeck : MonoBehaviour
{
    public bool IsResetInProgress { get; private set; }

    [SerializeField] private int _AngleStep = 15;
    [SerializeField] private List<GiraffeNeckElement> _NeckElements;

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
        _CurrentElementIndex = Mathf.Clamp(_CurrentElementIndex + 1, 0, _NeckElements.Count - 1);
    }

    public void MoveDown()
    {
        _CurrentElementIndex = Mathf.Clamp(_CurrentElementIndex - 1, 0, _NeckElements.Count - 1);
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
        }
    }
}