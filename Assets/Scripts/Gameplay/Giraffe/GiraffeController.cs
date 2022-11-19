using UnityEngine;

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
        _GiraffeNeck.RotateBone(value);
    }
}