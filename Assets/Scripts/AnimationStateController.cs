using UnityEngine;

public class AnimationStateController : MonoBehaviour {

    Animator animator;
    private bool _isWalking;
    private bool _isRunning;
    private bool _isJumping;
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isJumpingHash;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update() {

        //WALKING logic
        _isWalking = animator.GetBool(_isWalkingHash);
        _isRunning = animator.GetBool(_isRunningHash);
        _isJumping = animator.GetBool(_isJumpingHash);

        if (!_isWalking && Input.GetKeyDown(KeyCode.W)) {
            // Set the isWalking boolean to be true
            animator.SetBool(_isWalkingHash, true);
        }
        if (_isWalking && Input.GetKeyUp(KeyCode.W)) {
            // Set the isWalking boolean to be false
            animator.SetBool(_isWalkingHash, false);
        }

        //RUNNING logic
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            // Set the isRunning boolean to be true
            animator.SetBool(_isRunningHash, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            // Set the isRunning boolean to be false
            animator.SetBool(_isRunningHash, false);
        }

        //JUMPING logic
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool(_isJumpingHash, true);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            animator.SetBool(_isJumpingHash, false);
        }
    }
}
