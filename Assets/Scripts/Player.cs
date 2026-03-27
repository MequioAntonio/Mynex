using NUnit.Framework.Constraints;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public static Player Instance { get; set; }

    private float moveSpeed = 4f; // default è 4f, pc is 12f
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRendererOmbra;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private PlayerFootstepManager footstepManager;
    public Animator animator;
    public AudioSource audioSource;

    public Totem totemRaccoglibile;
    public Totem totemRaccolto;
    public TotemSlot slotPiazzabile;

    public Grammophone grammofonoSpegnibile;

    //public string[] colori = { "Rosso", "Giallo", "Verde", "Blu" };
    public int coloriUccisiProgress = 0; //default è 0

    //public string[] totems = { "Cuore", "Cervo", "Corona", "Porta" };
    public int totemPiazzatiProgress = 0; //default è 0

    //public string[] elementi = { "Acqua", "Fuoco", "Terra", "Vento" };
    public int grammofoniSpentiProgress = 0; //default è 0

    public bool elisir1 = false;
    public bool elisir2 = false;
    public bool elisir3 = false;

    public int pozioni = 1;

    //public bool caveGiaCaricata = false;

    private bool isWalkingRight;
    private bool isWalkingLeft;
    private bool isWalkingSouth;
    private bool isWalkingNorth;
    private bool canWalkAnimation;
    private float xPosLastFrame;
    private float yPosLastFrame;
    private float playerRadius;
    private float playerHeight;
    private Vector2 dimPlayer;

    private Vector2 targetPosition;
    private Vector2 previousPosition;
    private bool isMoving = false;
    private bool arrivato = false;
    private bool canControl;

    private float maxHealth = 100; // default è 100
    private float health = 100;
    private float attackRange = 1.5f;
    private int attackDamage = 10;
    private Vector2 lastMoveDirection = Vector2.right;

    private float footstepTimer = 0f;
    private float footstepInterval = 0.3f;

    private Rigidbody2D rb;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        xPosLastFrame = transform.position.x;
        yPosLastFrame = transform.position.y;
        canWalkAnimation = true;
        playerRadius = 0.5f;
        playerHeight = 1.5f;
        dimPlayer = new Vector2(playerRadius, playerHeight);

        targetPosition = transform.position;
        previousPosition = transform.position;
        canControl = true;
    }

    private void Start() {
        PersistentUI.Instance.healthbar.SetMaxHealth(maxHealth);
        PersistentUI.Instance.healthbar.SetCurrentHealth(health);

    }

    private void Update() {
        if (canWalkAnimation) {
            if (isMoving) {

                Vector2 currentPosition = rb.position;
                float moveDistance = moveSpeed * Time.deltaTime;

                Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveDistance);

                rb.MovePosition(newPosition);

                if (Vector2.Distance(currentPosition, targetPosition) < 0.01f) {
                    rb.MovePosition(targetPosition);
                    isMoving = false;
                    arrivato = true;
                }
            }
            else if (canControl) {

                Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
                Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

                float moveDistance = moveSpeed * Time.deltaTime;
                float skinWidth = 0.02f;
                float castDistance = moveDistance + skinWidth;

                Vector2 currentPosition = rb.position;
                Vector2 targetPosition = currentPosition;

                if (inputVector != Vector2.zero) {
                    lastMoveDirection = inputVector;
                }

                if (inputVector != Vector2.zero && footstepTimer <= 0f && footstepManager.groundTilemap != null) {
                    footstepManager.PlayFootstep(transform.position);
                    footstepTimer = footstepInterval;
                }

                bool canMoveFull = !Physics2D.CapsuleCast(
                    currentPosition,
                    dimPlayer,
                    CapsuleDirection2D.Vertical,
                    0f,
                    moveDir,
                    castDistance,
                    obstacleLayerMask
                );

                if (canMoveFull) {
                    targetPosition += moveDir * moveDistance;
                }
                else {
                    // fallback X
                    if (!Physics2D.CapsuleCast(
                        currentPosition,
                        dimPlayer,
                        CapsuleDirection2D.Vertical,
                        0f,
                        new Vector2(moveDir.x, 0f),
                        castDistance,
                        obstacleLayerMask
                    )) {
                        targetPosition += new Vector2(moveDir.x, 0f) * moveDistance;
                    }

                    // fallback Y
                    if (!Physics2D.CapsuleCast(
                        currentPosition,
                        dimPlayer,
                        CapsuleDirection2D.Vertical,
                        0f,
                        new Vector2(0f, moveDir.y),
                        castDistance,
                        obstacleLayerMask
                    )) {
                        targetPosition += new Vector2(0f, moveDir.y) * moveDistance;
                    }
                }

                rb.MovePosition(targetPosition);

                // Reset directions
                isWalkingRight = false;
                isWalkingLeft = false;
                isWalkingNorth = false;
                isWalkingSouth = false;

                if (inputVector != Vector2.zero) {
                    float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;

                    if (angle < 0) angle += 360;

                    if (angle >= 65f && angle < 115f) {
                        isWalkingNorth = true;
                    }
                    else if (angle >= 295f || angle < 65f) {
                        isWalkingRight = true;
                    }
                    else if (angle >= 245f && angle < 295f) {
                        isWalkingSouth = true;
                    }
                    else if (angle >= 115f && angle < 245f) {
                        isWalkingLeft = true;
                    }
                }
            }
        }

        if (!canControl) {
            AIUpdateMovementAnimation();
        }

        previousPosition = transform.position;

        if (footstepTimer > 0f)
            footstepTimer -= Time.deltaTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");

        if (spawnPoint != null) {
            transform.position = spawnPoint.transform.position;
        }
        else {
            Debug.LogWarning("Nessun PlayerSpawn trovato nella scena " + scene.name);
        }

        CinemachineCamera vcam = FindAnyObjectByType<CinemachineCamera>();
        if (vcam != null) {
            vcam.Follow = transform;
            vcam.LookAt = transform;
        }
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void MoveTo(Vector2 point) {
        targetPosition = point;
        isMoving = true;
        arrivato = false;
    }

    private void AIUpdateMovementAnimation() {
        if (isMoving) {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            if (direction != Vector2.zero && footstepTimer <= 0f) {
                footstepManager.PlayFootstep(transform.position);
                footstepTimer = footstepInterval;
            }

            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x)) {
                if (direction.y > 0) {
                    animator.SetBool("isWalkingNorth", true);
                    animator.SetBool("isWalkingSouth", false);
                }
                else {
                    animator.SetBool("isWalkingNorth", false);
                    animator.SetBool("isWalkingSouth", true);
                }

                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingLeft", false);
            }
            else {
                if (direction.x < 0) {
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isWalkingLeft", true);
                }
                else {
                    animator.SetBool("isWalkingLeft", false);
                    animator.SetBool("isWalking", true);
                }

                animator.SetBool("isWalkingNorth", false);
                animator.SetBool("isWalkingSouth", false);
            }
        }
        else {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingNorth", false);
            animator.SetBool("isWalkingSouth", false);
        }
    }

    public void Attack() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        float width = attackRange;
        float height = attackRange * 1.5f;

        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (height / 2f);
        Vector2 boxSize = new Vector2(width, height);

        foreach (Collider2D hit in hits) {
            Vector2 directionToEnemy = (hit.transform.position - transform.position).normalized;
            Vector2 facingDirection = lastMoveDirection;

            float angle = Vector2.Angle(facingDirection, directionToEnemy);

            if (angle <= 60f) {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null) {
                    damageable.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void TakeDamage(int damage) {
        GetComponentInChildren<CharacterEffects>()?.FlashRed();
        health -= damage;

        PersistentUI.Instance.healthbar.SetHealth(health);

        if (health <= 0) {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die() {
        animator.enabled = false;

        spriteRendererOmbra.enabled = false;

        int randomValue = Random.Range(1, 3);

        Sprite[] deathSprites = Resources.LoadAll<Sprite>($"Sprites/Nex/Nex_Death_{randomValue}");
        if (deathSprites.Length > 0) {
            spriteRenderer.sprite = deathSprites[0];
        }
        else {
            Debug.LogError($"Nex_Death_{randomValue} non ha sprite caricati!");
        }

        canWalkAnimation = false;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0;

        PersistentUI.Instance.ShowUI(false);
        PersistentUI.Instance.deathScreen.Show(true);
    }

    public bool IsWalkingRight() => isWalkingRight;
    public bool IsWalkingLeft() => isWalkingLeft;
    public bool IsWalkingSouth() => isWalkingSouth;
    public bool IsWalkingNorth() => isWalkingNorth;
    public void setCanWalkAnimation(bool canWalk) => this.canWalkAnimation = canWalk;

    public bool getCanWalkAnimation() {
        return canWalkAnimation;
    }

    public bool IsMoving() {
        return isMoving;
    }

    public void SetIsMoving(bool im) {
        isMoving = im;
    }

    public void SetCanControl(bool var) {
        canControl = var;

        if (!canControl) {
            isWalkingLeft = false;
            isWalkingRight = false;
            isWalkingNorth = false;
            isWalkingRight = false;
        }
    }

    public bool CanControl() {
        return canControl;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetHealth() {
        return health;
    }

    public void SetHealth(float health) {
        this.health = health;
    }

    public bool getArrivato() {
        return arrivato;
    }

    public void drinkPotion() {
        if (pozioni > 0) {
            health = health + 50;
            if (health > 100) {
                health = 100;
            }
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Drink_Potion"));
            PersistentUI.Instance.potionButton.GetComponentInChildren<PotionButton>().removePotion();
            PersistentUI.Instance.healthbar.SetHealth(health);
        }
    }

    /*public void mescolaColori() {
        colori = colori.OrderBy(c => Random.value).ToArray();
    }
    public void mescolaTotems() {
        totems = totems.OrderBy(c => Random.value).ToArray();
    }
    public void mescolaElementi() {
        elementi = elementi.OrderBy(c => Random.value).ToArray();
    }*/

    public void RaccogliTotem() {
        if (totemRaccoglibile.totemType == "Cuore" && !PlayerPossiedeUnTotem()) {
            PersistentUI.Instance.totemPortatoCuore.SetActive(true);
            totemRaccoglibile.transform.position = new Vector3(-80f, 80f, 0f);
            totemRaccolto = totemRaccoglibile;
            totemRaccoglibile = null;
        }
        else if (totemRaccoglibile.totemType == "Cervo" && !PlayerPossiedeUnTotem()) {
            PersistentUI.Instance.totemPortatoCervo.SetActive(true);
            totemRaccoglibile.transform.position = new Vector3(-80f, 80f, 0f);
            totemRaccolto = totemRaccoglibile;
            totemRaccoglibile = null;
        }
        else if (totemRaccoglibile.totemType == "Corona" && !PlayerPossiedeUnTotem()) {
            PersistentUI.Instance.totemPortatoCorona.SetActive(true);
            totemRaccoglibile.transform.position = new Vector3(-80f, 80f, 0f);
            totemRaccolto = totemRaccoglibile;
            totemRaccoglibile = null;
        }
        else if (totemRaccoglibile.totemType == "Porta" && !PlayerPossiedeUnTotem()) {
            PersistentUI.Instance.totemPortatoPorta.SetActive(true);
            totemRaccoglibile.transform.position = new Vector3(-80f, 80f, 0f);
            totemRaccolto = totemRaccoglibile;
            totemRaccoglibile = null;
        }
    }

    private bool PlayerPossiedeUnTotem() {

        if (!PersistentUI.Instance.totemPortatoCuore.activeInHierarchy && !PersistentUI.Instance.totemPortatoCervo.activeInHierarchy && !PersistentUI.Instance.totemPortatoCorona.activeInHierarchy && !PersistentUI.Instance.totemPortatoPorta.activeInHierarchy) {
            return false;
        }
        else {
            return true;
        }
    }

    public void PiazzaTotem() {
        totemRaccolto.transform.position = slotPiazzabile.totemPosition.position;

        PersistentUI.Instance.totemPortatoCervo.SetActive(false);
        PersistentUI.Instance.totemPortatoCuore.SetActive(false);
        PersistentUI.Instance.totemPortatoCorona.SetActive(false);
        PersistentUI.Instance.totemPortatoPorta.SetActive(false);

        if (slotPiazzabile.totemCorretto != totemRaccolto.totemType) {
            Time.timeScale = 0;
            PersistentUI.Instance.ShowUI(false);
            PersistentUI.Instance.deathScreen.Show(true);
        }

        slotPiazzabile.GetComponent<BoxCollider2D>().enabled = false;
        totemRaccolto.GetComponent<BoxCollider2D>().enabled = false;
        totemRaccolto = null;

        totemPiazzatiProgress++;
    }

    public void SpegniGrammofono() {
        grammofonoSpegnibile.GetComponent<AudioSource>().Stop();
        if (grammofonoSpegnibile.elemento == Loader.elementi[grammofoniSpentiProgress]) {
            grammofoniSpentiProgress++;
            grammofonoSpegnibile.triggerCollider.enabled = false;
        }
        else {
            Time.timeScale = 0;
            PersistentUI.Instance.ShowUI(false);
            PersistentUI.Instance.deathScreen.Show(true);
        }
    }
}
