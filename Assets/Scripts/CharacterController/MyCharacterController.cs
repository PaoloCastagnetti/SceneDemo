using UnityEngine;
using UnityEngine.InputSystem;

public class MyCharacterController : MonoBehaviour {
    [SerializeField]
    private IdContainer _IdProvider;

    private GameplayInputProvider _gameplayInputProvider;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float jumpHeight = 3f;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;


    private void Awake() {
        _gameplayInputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);

        controller = GetComponent<CharacterController>();
    }

    private void OnEnable() {
        _gameplayInputProvider.OnMove += MoveCharacter;
        _gameplayInputProvider.OnMoveCanceled += StopCharacter;
        _gameplayInputProvider.OnJump += JumpCharacter;
        _gameplayInputProvider.OnSprint += SprintCharacter;
        _gameplayInputProvider.OnSprintCanceled += SlowCharacter;
    }
    private void OnDisable() {
        _gameplayInputProvider.OnMove -= MoveCharacter;
        _gameplayInputProvider.OnJump -= JumpCharacter;
        _gameplayInputProvider.OnSprint -= SprintCharacter;
        _gameplayInputProvider.OnSprintCanceled -= SlowCharacter;
    }

    private void JumpCharacter() {
        Debug.Log("JUMP");
    }

    private void MoveCharacter(Vector2 vector2) {
        Debug.LogFormat("vector2.x: {0} || vector2.y: {1}",vector2.x,vector2.y);
        Debug.LogFormat("MOVE");
    }
    private void StopCharacter() {
        velocity = Vector3.zero;
        Debug.LogFormat("STOP");
    }

    private void SprintCharacter() {
        speed = 10;
        Debug.LogFormat("SPRINT");
    }

    private void SlowCharacter() { 
        speed = 5;
        Debug.LogFormat("SLOW DOWN");
    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Debug.LogFormat("IsGrounded: {0}", isGrounded);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) {
        //    speed = 10;
        //} else {
        //    speed = 5;
        //}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
