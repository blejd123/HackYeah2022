using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class GiraffeNeck : MonoBehaviour
{
    [SerializeField] private float _RotationMultiplier = 1.0f;
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

    public void RotateBone(float value)
    {
        _NeckElements[_CurrentElementIndex].RotateBone(-value * _RotationMultiplier);
    }

    private void Start()
    {
        if (_NeckElements.Count == 0)
        {
            _NeckElements = GetComponentsInChildren<GiraffeNeckElement>().ToList();
        }
    }
}