using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private BugSpawner bugSpawner;
    [SerializeField] private VoidEvent OnPlayerHit;
    [SerializeField] private VoidEvent OnPlayerMiss;
    [SerializeField] private VoidEvent OnStepForward;

    private GameInput input;

    private void Awake()
    {
        input = new GameInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Vertical.performed += OnPressVertical;
        input.Player.Horizontal.performed += OnPressHorizontal;
    }

    private void OnDisable()
    {
        input.Player.Vertical.performed -= OnPressVertical;
        input.Player.Horizontal.performed -= OnPressHorizontal;
        input.Disable();
    }

    private void OnPressVertical(InputAction.CallbackContext context)
    {
        Direction direction;
        float value = context.ReadValue<float>();
        if (value > 0f)
            direction = Direction.UP;
        else
            direction = Direction.DOWN;
        HitDirection(direction);
    }

    private void OnPressHorizontal(InputAction.CallbackContext context)
    {
        Direction direction;
        float value = context.ReadValue<float>();
        if (value > 0f)
            direction = Direction.RIGHT;
        else
            direction = Direction.LEFT;
        HitDirection(direction);
    }

    private void HitDirection(Direction direction)
    {
        Direction bugDirection = bugSpawner.GetCurrentBugDirection();

        if (bugDirection == Direction.NONE)
        {
            // Pass this turn    
        }
        else if (direction == bugDirection)
        {
            OnPlayerHit.Raise();
        }
        else // missed
        {
            OnPlayerMiss.Raise();
        }
        OnStepForward.Raise(); 
    }
}
