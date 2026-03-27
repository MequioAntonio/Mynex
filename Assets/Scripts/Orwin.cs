using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Orwin : MonoBehaviour {

    public static Orwin Instance { get; private set; }

    private const string IS_WALKING = "IsWalking";
    private const string IS_WALKING_LEFT = "IsWalkingLeft";
    private const string IS_WALKING_SOUTH = "IsWalkingDown";
    private const string IS_WALKING_NORTH = "IsWalkingUp";

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterEffects orwinCharacterEffects;
    [SerializeField] private FootstepManager footstepManager;

    private Vector2 previousPosition;
    private bool canWalkAnimation = true;
    private float moveSpeed = 3f;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private bool arrivato = false;
    private Vector3 destinazioneFinale = new Vector3(-2.784063f, -27.58018f, 0);

    private float footstepTimer = 0f;
    private float footstepInterval = 0.3f;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogWarning("Esiste già un altro Orwin!");
            Destroy(this);
            return;
        }
        Instance = this;

        previousPosition = transform.position;
    }

    private void Update() {
        if (canWalkAnimation) {
            if (isMoving) {
                Vector2 currentPosition = transform.position;
                Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
                transform.position = newPosition;

                if (Vector2.Distance(currentPosition, targetPosition) < 0.01f) {
                    transform.position = targetPosition;
                    isMoving = false;
                    arrivato = true;
                }
            }
        }
        UpdateMovementAnimation();
        previousPosition = transform.position;

    }

    public void MoveTo(Vector2 point) {
        targetPosition = point;
        isMoving = true;
        arrivato = false;
    }

    private void UpdateMovementAnimation() {
        if (isMoving) {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            if (direction != Vector2.zero && footstepTimer <= 0f) {
                footstepManager.PlayFootstep(transform.position);
                footstepTimer = footstepInterval;
            }

            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x)) {
                if (direction.y >= 0) {
                    animator.SetBool(IS_WALKING_NORTH, true);
                    animator.SetBool(IS_WALKING_SOUTH, false);
                }
                else {
                    animator.SetBool(IS_WALKING_NORTH, false);
                    animator.SetBool(IS_WALKING_SOUTH, true);
                }

                animator.SetBool(IS_WALKING, false);
                animator.SetBool(IS_WALKING_LEFT, false);
            }
            else {
                if (direction.x <= 0) {
                    animator.SetBool(IS_WALKING, false);
                    animator.SetBool(IS_WALKING_LEFT, true);
                }
                else {
                    animator.SetBool(IS_WALKING_LEFT, false);
                    animator.SetBool(IS_WALKING, true);
                }

                animator.SetBool(IS_WALKING_NORTH, false);
                animator.SetBool(IS_WALKING_SOUTH, false);
            }
        }
        else {
            animator.SetBool(IS_WALKING, false);
            animator.SetBool(IS_WALKING_LEFT, false);
            animator.SetBool(IS_WALKING_NORTH, false);
            animator.SetBool(IS_WALKING_SOUTH, false);
        }

        if (footstepTimer > 0f)
            footstepTimer -= Time.deltaTime;
    }

    public void setCanWalkAnimation(bool canWalk) {
        this.canWalkAnimation = canWalk;
    }

    public bool IsMoving() {
        return isMoving;
    }

    public bool getArrivato() {
        return arrivato;
    }

    public void setArrivato(bool arrivato) {
        this.arrivato = arrivato;
    }
}