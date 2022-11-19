using UnityEngine;
using DG.Tweening;

public sealed class GiraffeNeckElement : MonoBehaviour
{
    private Tweener _RotationTween;

    public void ResetRotation(float duration)
    {
        if (_RotationTween != null)
        {
            _RotationTween.Kill();
            _RotationTween = null;
        }

        transform.DOLocalRotateQuaternion(Quaternion.identity, duration);
    }

    public void RotateBone(float value)
    {
        if (_RotationTween != null && _RotationTween.IsActive())
        {
            return;
        }
        var rotation = transform.localRotation;
        rotation *= Quaternion.Euler(Vector3.forward * value);
        _RotationTween = transform.DOLocalRotateQuaternion(rotation, 0.1f);
    }
}