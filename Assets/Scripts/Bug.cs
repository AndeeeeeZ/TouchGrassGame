using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private VoidEvent OnBugReachedPlayer; 
    [SerializeField] private int stepSize;
    [SerializeField] private float moveSpeed;
    public Direction direction;
    private Vector3 targetPosition;

    public void Start()
    {
        targetPosition = transform.position;
    }

    public void Update()
    {
        // If reached player then destroy self
        if (targetPosition == Vector3.zero)
        {
            OnBugReachedPlayer.Raise(); 
            Destroy(gameObject); 
        }
        float dist = Vector3.Distance(transform.position, targetPosition);

        if (dist > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Snap to targetPosition if close enough
            transform.position = targetPosition;
        }

    }

    public void Initialize(Vector3 startPosition, Direction currDirection)
    {
        direction = currDirection;
        targetPosition = startPosition;
    }

    public void StepForward()
    {
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

}
