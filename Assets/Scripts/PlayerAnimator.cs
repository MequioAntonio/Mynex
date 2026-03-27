using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "isWalking";
    private const string IS_WALKING_LEFT = "isWalkingLeft";
    private const string IS_WALKING_SOUTH = "isWalkingSouth";
    private const string IS_WALKING_NORTH = "isWalkingNorth";
    private const string ATTACK = "Attack";

    public Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }

    private void GameInput_OnAttackAction(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }

    private void Update() {
        if (Player.Instance == null) return;

        if (!Player.Instance.IsMoving() && Player.Instance.CanControl()) {
            animator.SetBool(IS_WALKING, Player.Instance.IsWalkingRight());
            animator.SetBool(IS_WALKING_LEFT, Player.Instance.IsWalkingLeft());
            animator.SetBool(IS_WALKING_SOUTH, Player.Instance.IsWalkingSouth());
            animator.SetBool(IS_WALKING_NORTH, Player.Instance.IsWalkingNorth());
        }
    }

    private void OnDestroy() {
        if (GameInput.Instance != null) {
            GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
        }
    }

}
