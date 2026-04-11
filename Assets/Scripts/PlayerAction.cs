using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private BugSpawner bugSpawner;
    [SerializeField] private VoidEvent OnPlayerHit;
    [SerializeField] private VoidEvent OnPlayerMiss;
    [SerializeField] private VoidEvent OnStepForward;
    [SerializeField] private RandomPitchAudioClipPlayer clipPlayer; 
    [SerializeField] private AudioClip[] attackSoundEffects; 
    [SerializeField] private AudioClip hurtSoundEffect; 

    private GameInput input;
    private Animator animator; 

    private void Awake()
    {
        input = new GameInput();
        animator = GetComponent<Animator>(); 
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
        Bug bug = bugSpawner.GetCurrentBug();
        
        PlayAttackAnimation(direction); 


        if (bugDirection == Direction.NONE)
        {
            // Pass this turn    
            clipPlayer.PlayClip(GetRandomAttackAudioClip()); 
        }
        else if (direction == bugDirection)
        {
            OnPlayerHit.Raise();

            if (bug != null)
                bug.GetHitByPlayer(); 
            
            clipPlayer.PlayClip(GetRandomAttackAudioClip()); 
        }
        else // missed
        {
            OnPlayerMiss.Raise();
            clipPlayer.PlayClip(hurtSoundEffect); 
        }
        OnStepForward.Raise(); 
    }

    private AudioClip GetRandomAttackAudioClip()
    {
        int i = Random.Range(0, attackSoundEffects.Length); 
        return attackSoundEffects[i]; 
    }

    private void PlayAttackAnimation(Direction direction)
    {
        string animationName = "Idle"; 
        switch (direction)
        {
            case Direction.UP:
                animationName = "UpAttack"; 
                break; 
            case Direction.DOWN:
                animationName = "DownAttack"; 
                break; 
            case Direction.LEFT: 
                animationName = "LeftAttack"; 
                break; 
            case Direction.RIGHT: 
                animationName = "RightAttack"; 
                break; 
        }
        animator.Play(animationName); 
    }
}
