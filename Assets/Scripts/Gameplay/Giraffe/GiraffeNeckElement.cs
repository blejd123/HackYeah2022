using UnityEngine;

public sealed class GiraffeNeckElement : MonoBehaviour
{
    [SerializeField] private float _RotationMultiplier = 1.0f;

    public void RotateBone(float value)
    {
        var rotation = transform.localRotation;
        rotation *= Quaternion.Euler(Vector3.forward * value * _RotationMultiplier);
        transform.localRotation = rotation;
    }
}