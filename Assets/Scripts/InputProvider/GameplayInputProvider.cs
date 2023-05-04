using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputProvider : InputProvider {
    #region Delegate
    public OnVector2Delegate OnMove;
    public OnVoidDelegate OnMoveCanceled;
    public OnVoidDelegate OnJump;
    public OnVoidDelegate OnJumpCanceled;
    public OnVoidDelegate OnSprint;
    public OnVoidDelegate OnSprintCanceled;
    #endregion

    [Header("Gameplay")]
    [SerializeField]
    private InputActionReference _Move;

    [SerializeField]
    private InputActionReference _Jump;

    [SerializeField]
    private InputActionReference _Sprint;

    private void OnEnable() {
        _Move.action.Enable();
        _Jump.action.Enable();
        _Sprint.action.Enable();

        _Move.action.performed += MovePerfomed;
        _Move.action.canceled += MoveCanceled;
        _Jump.action.performed += JumpPerfomed;
        _Jump.action.canceled += JumpCanceled;
        _Sprint.action.performed += SprintPerformed;
        _Sprint.action.canceled += SprintCanceled;
    }

    private void OnDisable() {
        _Move.action.Disable();
        _Jump.action.Disable();
        _Sprint.action.Disable();

        _Move.action.performed -= MovePerfomed;
        _Move.action.canceled -= MoveCanceled;
        _Jump.action.performed -= JumpPerfomed;
        _Sprint.action.performed -= SprintPerformed;
        _Sprint.action.canceled += SprintCanceled;
    }

    private void MovePerfomed(InputAction.CallbackContext obj) {
        Vector2 value = obj.action.ReadValue<Vector2>();
        OnMove?.Invoke(value);
    }

    private void MoveCanceled(InputAction.CallbackContext obj) {
        OnMoveCanceled?.Invoke();
    }

    private void JumpPerfomed(InputAction.CallbackContext obj) {
        OnJump?.Invoke();
    }

    private void JumpCanceled(InputAction.CallbackContext obj) {
        OnJumpCanceled?.Invoke();
    }
    private void SprintPerformed(InputAction.CallbackContext obj) {
        OnSprint?.Invoke();
    }
    private void SprintCanceled(InputAction.CallbackContext obj) {
        OnSprintCanceled?.Invoke();
    }
}
