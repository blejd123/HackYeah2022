using UnityEngine;
using DG.Tweening;

public sealed class BlinkColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer _Renderer;

    private void Start()
    {
        var color = _Renderer.material.color;
        var target = color;
        target.a = 0.1f;
        _Renderer.material.DOColor(target, 0.25f).SetLoops(-1, LoopType.Yoyo);
    }
}