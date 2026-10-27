using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 6f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Gravity")]
    [SerializeField] private float baseGravityScale = 1f;
    [SerializeField] private float fallMultiplier = 3f;

    [Header("GroundChecker")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private float jumpBufferTime = 0.15f;

    [Header("Dash")]
    [SerializeField] private int amountOfDashes = 1;
    [SerializeField] private float AimTimeScale = 0.3f;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    // Components
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private LineRenderer aimLine;

    // Movement
    private float moveInput;
    private bool isGrounded;
    private bool spaceReleased;
    private float jumpBufferTimer;

    // Dash and aiming
    private bool aiming;
    private bool isDashing;
    private bool aimPressed;
    private bool aimReleased;
    private Vector3 mouseScreenPosition;
    private Vector2 aimDirection;
    private float dashTimeLeft;

    // Time settings before aiming
    private float previousTimeScale;
    private float previousFixedDeltaTime;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        aimLine = GetComponent<LineRenderer>();

        rb.gravityScale = baseGravityScale;
        aimLine.positionCount = 2;
        aimLine.useWorldSpace = true;
        aimLine.enabled = false;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying()) return;

        jumpBufferTimer = Mathf.Max(0f, jumpBufferTimer - Time.deltaTime);

        ReadMovementInput();
        TryStartAiming();
        UpdateAimDirection();
        TryStartDash();
        UpdateSprite();
        UpdateAimLine();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsPlaying()) return;

        if (isDashing)
        {
            UpdateDash();
        }
        else
        {
            CheckGround();
            Move();
            TryJump();
            TryCutJump();
            UpdateGravity();
        }

        spaceReleased = false;
    }

    private void ReadMovementInput()
        {
        moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) moveInput -= 1f;
        if (Input.GetKey(KeyCode.D)) moveInput += 1f;

        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferTimer = jumpBufferTime;
        if (Input.GetKeyUp(KeyCode.Space))
            spaceReleased = true;
        aimPressed = Input.GetMouseButtonDown(1);
        aimReleased = Input.GetMouseButtonUp(1);
        mouseScreenPosition = Input.mousePosition;
    }

    private void TryStartAiming()
    {
        if (!aimPressed || aiming || isDashing || amountOfDashes <= 0)
            return;

        aiming = true;
        previousTimeScale = Time.timeScale;
        previousFixedDeltaTime = Time.fixedDeltaTime;

        Time.timeScale = AimTimeScale;
        Time.fixedDeltaTime = previousFixedDeltaTime * AimTimeScale;
    }

    private void UpdateAimDirection()
    {
        if (!aiming || isDashing) return;

        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        Vector3 screenPosition = mouseScreenPosition;
        screenPosition.z = transform.position.z - mainCamera.transform.position.z;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        aimDirection = ((Vector2)mouseWorldPosition - rb.position).normalized;
    }

    private void TryStartDash()
    {
        if (!aimReleased || !aiming) return;

        StopAiming();

        if (aimDirection.sqrMagnitude == 0f) return;

        isDashing = true;
        amountOfDashes--;
        dashTimeLeft = dashDuration;
    }

    private void StopAiming()
    {
        if (!aiming) return;

        aiming = false;
        Time.timeScale = previousTimeScale;
        Time.fixedDeltaTime = previousFixedDeltaTime;
        aimLine.enabled = false;
    }

    private void CheckGround()
    {
        Bounds bounds = playerCollider.bounds;
        Vector2 origin = new Vector2(bounds.center.x, bounds.min.y + 0.05f);
        Vector2 size = new Vector2(bounds.size.x * 0.9f, 0.05f);

        RaycastHit2D hit = Physics2D.BoxCast(
            origin, size, 0f, Vector2.down, groundCheckDistance, groundLayer);

        isGrounded = hit.collider != null;
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void TryJump()
    {
        if (jumpBufferTimer <= 0f || !isGrounded) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        jumpBufferTimer = 0f;
    }

    private void TryCutJump()
    {
        if (!spaceReleased || rb.linearVelocity.y <= 0f) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2f);
    }

    private void UpdateGravity()
    {
        rb.gravityScale = rb.linearVelocity.y < 0f
            ? baseGravityScale * fallMultiplier
            : baseGravityScale;
    }

    private void UpdateDash()
    {
        if (dashTimeLeft > 0f)
        {
            dashTimeLeft -= Time.fixedDeltaTime;
            rb.linearVelocity = aimDirection * dashSpeed;
            rb.gravityScale = 0f;
        }
        else
        {
            isDashing = false;
            rb.gravityScale = baseGravityScale;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void UpdateSprite()
    {
        if (moveInput != 0f)
            spriteRenderer.flipX = moveInput < 0f;
    }

    private void UpdateAimLine()
    {
        aimLine.enabled = aiming && !isDashing;
        if (!aimLine.enabled) return;

        Vector2 endPoint = rb.position + aimDirection * dashSpeed * dashDuration;
        aimLine.SetPosition(0, transform.position);
        aimLine.SetPosition(1, new Vector3(endPoint.x, endPoint.y, transform.position.z));
    }

    private void OnDisable()
    {
        // Do not undo a pause or a time change made by another system.
        if (aiming)
        {
            if (Mathf.Approximately(Time.timeScale, AimTimeScale))
                Time.timeScale = previousTimeScale;

            if (Mathf.Approximately(Time.fixedDeltaTime, previousFixedDeltaTime * AimTimeScale))
                Time.fixedDeltaTime = previousFixedDeltaTime;
        }

        aiming = false;
        isDashing = false;
        spaceReleased = false;
        jumpBufferTimer = 0f;
        dashTimeLeft = 0f;

        if (aimLine != null) aimLine.enabled = false;
        if (rb != null) rb.gravityScale = baseGravityScale;
    }

    public int DashCount()
    {
        return amountOfDashes;
    }

    public bool IsAiming()
    {
        return aiming;
    }

    public void AddDash()
    {
        if (amountOfDashes == 0)//redo
            amountOfDashes++;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}
