using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private VoidEvent OnBugReachedPlayer;
    [SerializeField] private int stepSize;
    [SerializeField] private float moveSpeed, knockbackDistance;
    public Direction direction;
    private ParticleSystem particles;
    private Animator animator;
    private Vector3 targetPosition;
    private SpriteRenderer sr;
    private bool isAlive;

    public void Initialize(Vector3 startPosition, Direction currDirection, Sprite sprite)
    {
        direction = currDirection;
        targetPosition = startPosition;
        sr.sprite = sprite; 

        if (direction == Direction.LEFT || direction == Direction.DOWN)
            sr.flipX = true;
    }

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        isAlive = true;
        targetPosition = transform.position;
    }

    public void Update()
    {
        // If reached player then get knockback
        if (isAlive && HasReachedOrPassedCenter())
        {
            OnBugReachedPlayer.Raise();
            isAlive = false;
            animator.Play("FadeOut");

            targetPosition = Vector3.zero;
        }

        float dist = Vector3.Distance(transform.position, targetPosition);

        if (dist > 0.001f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Snap to targetPosition if close enough
            transform.position = targetPosition;
        }
    }

    public void GetHitByPlayer()
    {
        OnBugReachedPlayer.Raise();
        isAlive = false;
        particles.Play();
        animator.Play("FadeOut");

        // Knock back player
        targetPosition = -1f * knockbackDistance * GetMoveDirection();

        // Offset to in direction perpendicular to the direction of movement
        Vector2 offset = GetMoveDirection();
        offset = new Vector2(Mathf.Abs(offset.x), Mathf.Abs(offset.y));
        offset = Vector2.one - offset;

        float sideAmount = Random.Range(-0.75f, 0.75f);
        offset *= sideAmount * knockbackDistance;

        targetPosition += (Vector3)offset;
        float r = Random.Range(0.75f, 1.25f);
        targetPosition *= r;
    }


    public void StepForward()
    {
        if (!isAlive) return;

        Vector2 moveDirection = GetMoveDirection();
        targetPosition += (Vector3)(moveDirection * stepSize);
    }

    // Move towards the center
    private Vector2 GetMoveDirection()
    {
        switch (direction)
        {
            case Direction.UP:
                return Vector2.down;

            case Direction.DOWN:
                return Vector2.up;

            case Direction.LEFT:
                return Vector2.right;

            case Direction.RIGHT:
                return Vector2.left;

            default:
                return Vector2.zero;
        }
    }
    private bool HasReachedOrPassedCenter()
    {
        switch (direction)
        {
            case Direction.UP:
                return targetPosition.y <= 0f;

            case Direction.DOWN:
                return targetPosition.y >= 0f;

            case Direction.LEFT:
                return targetPosition.x >= 0f;

            case Direction.RIGHT:
                return targetPosition.x <= 0f;

            default:
                return false;
        }
    }

    // If one step away from the player in center
    public bool IsReachableByPlayer()
    {
        return targetPosition == -1f * (Vector3)GetMoveDirection();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
