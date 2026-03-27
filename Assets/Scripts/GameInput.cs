using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; set; }
    public event EventHandler OnAttackAction;

    [SerializeField] private bool useTouchControls = false;

    private Player player;
    private PlayerInputActions playerInputActions;
    private bool actionsBound = false;

    private void Awake() {
        
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerInputActions = new PlayerInputActions();

        BindActions();

        playerInputActions.Player.Enable();
    }

    private void BindActions() {
        if (actionsBound || playerInputActions == null) return;
        if (!useTouchControls) playerInputActions.Player.Attack.performed += Attack_performed;
        actionsBound = true;
    }

    private void UnbindActions() {
        if (!actionsBound || playerInputActions == null) return;
        if (!useTouchControls) playerInputActions.Player.Attack.performed -= Attack_performed;
        actionsBound = false;
    }

    private void OnEnable() {

        if (playerInputActions == null) return;
        playerInputActions.Player.Enable();
        BindActions();
    }

    private void OnDisable() {

        if (playerInputActions == null) return;
        UnbindActions();
        playerInputActions.Player.Disable();
    }

    private void OnDestroy() {

        if (playerInputActions != null) {
            UnbindActions();
            playerInputActions.Dispose();
            playerInputActions = null;
        }

        if (Instance == this) Instance = null;
    }

    private void Attack_performed(InputAction.CallbackContext ctx) {
        if (player != null && player.CanControl()) {
            OnAttackAction?.Invoke(this, EventArgs.Empty);
            player.setCanWalkAnimation(false);
        }
    }

    public void TriggerAttackButton() {
        if (player != null && player.CanControl()) {
            OnAttackAction?.Invoke(this, EventArgs.Empty);
            player.setCanWalkAnimation(false);
        }
    }

    public Vector2 GetMovementVectorNormalized() {
        if (useTouchControls && PersistentUI.Instance.joystick != null) {
            Vector2 inputVector = PersistentUI.Instance.joystick.Direction();
            return inputVector.normalized;
        }
        else {
            if (playerInputActions == null) return Vector2.zero;
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }
    }

    public void SetPlayer(Player p) {
        player = p;
    }
    public void SetJoystick(Joystick j) {
        PersistentUI.Instance.joystick = j;
    }
}