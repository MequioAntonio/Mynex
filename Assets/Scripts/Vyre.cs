using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Vyre : MonoBehaviour, IDamageable {

    private const string IS_WALKING = "IsWalking";
    private const string IS_WALKING_LEFT = "IsWalkingLeft";
    private const string IS_WALKING_SOUTH = "IsWalkingSouth";
    private const string IS_WALKING_NORTH = "IsWalkingNorth";
    private const string DEATH = "Death";
    
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vyre_Attack vyreAttack;
    [SerializeField] private bool playAttackAnim = false;
    [SerializeField] private int attackAnimDirection = 0;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FootstepManager footstepManager;
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private bool boss = false;

    public Transform target;
    public bool idle = false;
    public string colore = "Standard";

    private NavMeshAgent agent;
    private bool canWalkAnimation = true;
    private bool dead = false;
    private float health = 10;
    private float attackRange = 1.5f;
    private float attackCooldown = 3f;
    private float lastAttackTime;
    public int attackDamage;
    AnimatorStateInfo stateInfo;

    private float footstepTimer = 0f;
    private float footstepInterval = 0.3f;

    private void Awake() {
        if (boss) {
            attackDamage = 20;
            health = 30;
        }
        else {
            attackDamage = 10;
            health = 10;
        }
    }

    private void Start() {
        audioSource.volume = 0.3f;

        EnemyManager.Instance.RegisterEnemy(gameObject);

        StartCoroutine(PlayIdleSoundRoutine());

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (target == null) {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) {
                target = playerObj.GetComponent<Transform>();
            }
            else {
                Debug.LogError("Nessun Player trovato con il tag 'Player'!");
            }
        }

        if (playAttackAnim) {
            if(attackAnimDirection == 1) {
                StartCoroutine(GuardaSinistra());
            }
            else if(attackAnimDirection == 2) {
                StartCoroutine(GuardaDestra());
            }
            else if(attackAnimDirection == 3) {
                StartCoroutine(GuardaSopra());
            }
        }
    }

    private void Update() {
        if (!dead) {
            if (!playAttackAnim) {
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);

                if (canWalkAnimation) {
                    if (distanceToPlayer <= attackRange && !idle) {
                        agent.isStopped = true;
                        animator.SetBool(IS_WALKING, false);

                        if (Time.time > lastAttackTime + attackCooldown) {
                            vyreAttack.StartAttack();
                            lastAttackTime = Time.time;
                        }
                    }
                    else {
                        agent.isStopped = false;
                        agent.SetDestination(target.position);

                        UpdateMovementAnimation();
                    }
                }
            }
            else {
                if (Time.time > lastAttackTime + attackCooldown) {
                    animator.SetTrigger("Attack_Fake");

                    int rand = Random.Range(0, 2);
                    if (rand == 0) {
                        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Attacks/Vyre/vyre_attack"));
                    }
                    else {
                        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Attacks/Vyre/vyre_attack_2"));
                    }

                    lastAttackTime = Time.time - Random.Range(0, 4);
                }
            }
        }
    }

    IEnumerator GuardaSinistra() {
        animator.SetBool(IS_WALKING_LEFT, true);
        yield return new WaitForSeconds(1f);
        animator.SetBool(IS_WALKING_LEFT, false);
    }
    IEnumerator GuardaDestra() {
        animator.SetBool(IS_WALKING, true);
        yield return new WaitForSeconds(1f);
        animator.SetBool(IS_WALKING, false);
    }
    IEnumerator GuardaSopra() {
        animator.SetBool(IS_WALKING_NORTH, true);
        yield return new WaitForSeconds(1f);
        animator.SetBool(IS_WALKING_NORTH, false);
    }

    public void TakeDamage(float damage) {
        GetComponentInChildren<FlashOnly>()?.FlashRed();

        health = health - damage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        if(boss) {
            if (Loader.colori[Player.Instance.coloriUccisiProgress] == colore) {
                Player.Instance.coloriUccisiProgress++;
            }
            else {
                Time.timeScale = 0;
                PersistentUI.Instance.ShowUI(false);
                PersistentUI.Instance.deathScreen.Show(true);
            }
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        if (rb2D != null) rb2D.simulated = false;
        agent.enabled = false;
        dead = true;
        canWalkAnimation = false;
        animator.SetBool(DEATH, true);

        audioSource.Stop();

        int rand = Random.Range(0, 3);
        if (rand == 0) {
            AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/Deaths/Vyre/vyre_death"), transform.position, 0.8f, 1f, 20f);
        }
        else if (rand == 1) {
            AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/Deaths/Vyre/vyre_death_2"), transform.position, 0.8f, 1f, 20f);
        }
        else {
            AudioManager.Instance.PlaySoundAtPosition(Resources.Load<AudioClip>("Sounds/Deaths/Vyre/vyre_death_3"), transform.position, 0.8f, 1f, 20f);
        }

        EnemyManager.Instance.UnregisterEnemy(gameObject);
        Destroy(gameObject, 1f);
    }

    private void UpdateMovementAnimation() {
        Vector3 moveDir = agent.desiredVelocity.normalized;

        if (moveDir != Vector3.zero && footstepTimer <= 0f) {
            footstepManager.PlayFootstep(transform.position);
            footstepTimer = footstepInterval;
        }

        if (agent.desiredVelocity.magnitude > 0.1f) {
            if (Mathf.Abs(moveDir.y) > Mathf.Abs(moveDir.x)) {
                if (moveDir.y > 0) {
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
                if(moveDir.x < 0) {
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

    private IEnumerator PlayIdleSoundRoutine() {
        while (!dead) {
            float waitTime = Random.Range(5f, 20f);
            yield return new WaitForSeconds(waitTime);

            if (!dead && idleSounds.Length > 0) {
                AudioClip clip = idleSounds[Random.Range(0, idleSounds.Length)];
                AudioManager.Instance.PlaySoundAtPosition(clip, transform.position, 0.8f, 1f, 20f);
            }
        }
    }

    public void setCanWalkAnimation(bool canWalk) {
        this.canWalkAnimation = canWalk;
    }

    public bool IsPlayingAttackAnim() {
        return playAttackAnim;
    }

    public void SetPlayingAttackAnim(bool a) {
        playAttackAnim = a;
    }

    public bool IsDead() {
        return dead;
    }
}