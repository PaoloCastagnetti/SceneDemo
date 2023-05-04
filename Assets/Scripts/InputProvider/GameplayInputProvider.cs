using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputProvider : InputProvider {
    #region Delegate
    public OnVector2Delegate OnMove;
    public OnVoidDelegate OnJump;
    public OnVoidDelegate OnSprint;
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
        _Jump.action.performed += JumpPerfomed;
        _Sprint.action.performed += SprintPerformed;
    }

    private void OnDisable() {
        _Move.action.Disable();
        _Jump.action.Disable();
        _Sprint.action.Disable();

        _Move.action.performed -= MovePerfomed;
        _Jump.action.performed -= JumpPerfomed;
        _Sprint.action.performed -= SprintPerformed;
    }

    private void MovePerfomed(InputAction.CallbackContext obj) {
        Vector2 value = obj.action.ReadValue<Vector2>();
        OnMove?.Invoke(value);
    }

    private void JumpPerfomed(InputAction.CallbackContext obj) {
        OnJump?.Invoke();
    }
    private void SprintPerformed(InputAction.CallbackContext obj) {
        OnSprint?.Invoke();
    }
}
