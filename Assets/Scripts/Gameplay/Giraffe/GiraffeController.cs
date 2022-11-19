using UnityEngine;
using Zenject;

public sealed class GiraffeController : MonoBehaviour
{
    [SerializeField] private GiraffeNeck _GiraffeNeck;
    [SerializeField] private float _MoveMultiplier = 1.0f;

    public void MoveHorizontally(float value)
    {
        var position = transform.position;
        position.x += value * _MoveMultiplier;
        transform.position = position;
    }

    public void MoveNeckSelection(float value)
    {
        if (value > 0.0f)
        {
            _GiraffeNeck.MoveUp();
        }

        if (value < 0.0f)
        {
            _GiraffeNeck.MoveDown();
        }
    }

    public void RotateNeckBone(float value)
    {
        if (value > 0.0f)
        {
            _GiraffeNeck.RotateRight();
        }

        if (value < 0.0f)
        {
            _GiraffeNeck.RotateLeft();
        }
    }

    public void ResetNeck()
    {
        if (_GiraffeNeck.IsResetInProgress)
        {
            return;
        }

        StartCoroutine(_GiraffeNeck.ResetNeck());
    }

    public sealed class Factory : PlaceholderFactory<Object, GiraffeController>
    {
    }
}