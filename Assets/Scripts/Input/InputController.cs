using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InputController : MonoBehaviour, InputActions.IPlayerActions
{
    [SerializeField] private List<GiraffeController> _Giraffes;
    private InputActions _InputActions;

    private int _CurrentGiraffeIndex;

    private float _MoveValue;
    private float _RotateValue;

    public void Register(GiraffeController girafeeController)
    {
        _Giraffes.Add(girafeeController);
    }

    public void Unregister(GiraffeController giraffeController)
    {
        _Giraffes.Remove(giraffeController);
    }

    private void OnEnable()
    {
        if (_InputActions == null)
        {
            _InputActions = new InputActions();
            _InputActions.Player.SetCallbacks(this);
        }

        _InputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _InputActions.Player.Disable();
    }

    private void Update()
    {
        _Giraffes[_CurrentGiraffeIndex].MoveHorizontally(_MoveValue * Time.deltaTime);
    }

    void InputActions.IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        _MoveValue = context.ReadValue<float>();
    }

    void InputActions.IPlayerActions.OnNeckChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _Giraffes[_CurrentGiraffeIndex].MoveNeckSelection(context.ReadValue<float>() * Time.deltaTime);
        }
    }

    void InputActions.IPlayerActions.OnNeckRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _Giraffes[_CurrentGiraffeIndex].RotateNeckBone(context.ReadValue<float>() * Time.deltaTime);
        }
    }

    void InputActions.IPlayerActions.OnNextGiraffe(InputAction.CallbackContext context)
    {
        if (context.performed && !Keyboard.current.shiftKey.isPressed)
        {
            _CurrentGiraffeIndex = Mathf.Clamp(_CurrentGiraffeIndex + 1, 0, _Giraffes.Count - 1);
        }
    }

    void InputActions.IPlayerActions.OnPreviousGiraffe(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _CurrentGiraffeIndex = Mathf.Clamp(_CurrentGiraffeIndex - 1, 0, _Giraffes.Count - 1);
        }
    }

    void InputActions.IPlayerActions.OnResetNeck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _Giraffes[_CurrentGiraffeIndex].ResetNeck();
        }
    }
}