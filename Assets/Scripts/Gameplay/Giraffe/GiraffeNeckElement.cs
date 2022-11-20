using UnityEngine;
using DG.Tweening;

public sealed class GiraffeNeckElement : MonoBehaviour
{
    [SerializeField] private GameObject _NeckSpherePrefab;

    private Tweener _RotationTween;
    private GameObject _NeckSphere;

    public void Select()
    {
        if (_NeckSphere == null)
        {
            _NeckSphere = Instantiate(_NeckSpherePrefab, transform);
        }
        _NeckSphere.SetActive(true);
    }

    public void Deselect()
    {
        if (_NeckSphere != null)
        {
            _NeckSphere.SetActive(false);
        }
    }

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