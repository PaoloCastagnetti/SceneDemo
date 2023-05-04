using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;
using UnityEngine.TextCore.Text;

public class AnimationStateController : MonoBehaviour {

    [SerializeField]
    private IdContainer _IdProvider;

    Animator animator;

    private GameplayInputProvider _gameplayInputProvider;

    private bool _isWalking;
    private bool _isRunning;
    private bool _isJumping;
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isJumpingHash;

    private void Awake() {
        _gameplayInputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);
    }

    private void OnEnable() {
        _gameplayInputProvider.OnMove += MoveAnimation;
        _gameplayInputProvider.OnMoveCanceled += CancelAnimation;
        _gameplayInputProvider.OnJump += JumpAnimation;
        _gameplayInputProvider.OnJumpCanceled += CancelAnimation;
        _gameplayInputProvider.OnSprint += SprintAnimation;
        _gameplayInputProvider.OnSprintCanceled += CancelAnimation;
    }
    private void OnDisable() {
        _gameplayInputProvider.OnMove -= MoveAnimation;
        _gameplayInputProvider.OnMoveCanceled -= CancelAnimation;
        _gameplayInputProvider.OnJump -= JumpAnimation;
        _gameplayInputProvider.OnJumpCanceled -= CancelAnimation;
        _gameplayInputProvider.OnSprint -= SprintAnimation;
        _gameplayInputProvider.OnSprintCanceled -= CancelAnimation;
    }

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("jumpPerformed");
    }

    private void MoveAnimation(Vector2 vector2) {
        _isWalking = animator.GetBool(_isWalkingHash);
        if (!_isWalking) {
            animator.SetBool(_isWalkingHash, true);
        }
    }

    private void JumpAnimation() {
        _isJumping = animator.GetBool(_isJumpingHash);
        if (!_isJumping) {
            animator.SetBool(_isJumpingHash, true);
        }
    }

    private void SprintAnimation() {
        _isRunning = animator.GetBool(_isRunningHash);
        if (!_isRunning) {
            animator.SetBool(_isRunningHash, true);
        }
    }

    private void CancelAnimation() {
        _isWalking = animator.GetBool(_isWalkingHash);
        _isRunning = animator.GetBool(_isRunningHash);
        _isJumping = animator.GetBool(_isJumpingHash);
        if (_isRunning)
            animator.SetBool(_isRunningHash, false);
        if (_isJumping)
            animator.SetBool(_isJumpingHash, false);
        if (_isWalking)
            animator.SetBool(_isWalkingHash, false);
    }

}
