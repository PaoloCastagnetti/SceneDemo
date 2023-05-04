using UnityEngine;

public class MyCharacterController : MonoBehaviour {
    [SerializeField]
    private IdContainer _IdProvider;

    private GameplayInputProvider _gameplayInputProvider;

    [SerializeField]
    private CharacterController controller;


    private void Awake() {
        _gameplayInputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);
    }

    private void OnEnable() {
        _gameplayInputProvider.OnMove += MoveCharacter;
        _gameplayInputProvider.OnJump += JumpCharacter;
        _gameplayInputProvider.OnSprint += SprintCharacter;
    }
    private void OnDisable() {
        _gameplayInputProvider.OnMove -= MoveCharacter;
        _gameplayInputProvider.OnJump -= JumpCharacter;
        _gameplayInputProvider.OnSprint -= SprintCharacter;
    }

    private void JumpCharacter() {
        Debug.Log("JUMP");
    }

    private void MoveCharacter(Vector2 vector2) {
        Debug.LogFormat("vector2.x: {0} || vector2.y: {1}",vector2.x,vector2.y);
        Debug.LogFormat("MOVE");
    }

    private void SprintCharacter() {
        Debug.LogFormat("SPRINT");
    }
}
