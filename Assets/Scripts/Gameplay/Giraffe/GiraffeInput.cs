using UnityEngine;
using Zenject;

[RequireComponent(typeof(GiraffeController))]
public sealed class GiraffeInput : MonoBehaviour
{
    [Inject] private InputController _InputController;

    private GiraffeController _GiraffeController;

    private void Awake()
    {
        _GiraffeController = GetComponent<GiraffeController>();
    }

    private void OnEnable()
    {
        _InputController.Register(_GiraffeController);
    }

    private void OnDisable()
    {
        _InputController.Unregister(_GiraffeController);
    }
}