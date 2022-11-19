using UnityEngine;
using DG.Tweening;

public sealed class GiraffeNeckElement : MonoBehaviour
{
    private Tweener _RotationTween;

    public void RotateBone(float value)
    {
        if (_RotationTween != null && _RotationTween.IsPlaying())
        {
            return;
        }
        var rotation = transform.localRotation;
        rotation *= Quaternion.Euler(Vector3.forward * value);
        _RotationTween = transform.DOLocalRotateQuaternion(rotation, 0.1f);
    }
}