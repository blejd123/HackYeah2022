using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GiraffeNeck : MonoBehaviour
{
    [SerializeField] private int _AngleStep = 15;
    [SerializeField] private List<GiraffeNeckElement> _NeckElements;

    private int _CurrentElementIndex;

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
        _NeckElements[_CurrentElementIndex].RotateBone(_AngleStep);
    }

    public void RotateRight()
    {
        _NeckElements[_CurrentElementIndex].RotateBone(-_AngleStep);
    }

    private void Start()
    {
        if (_NeckElements.Count == 0)
        {
            _NeckElements = GetComponentsInChildren<GiraffeNeckElement>().ToList();
        }
    }
}