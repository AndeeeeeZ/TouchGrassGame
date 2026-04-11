using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndScreenButtons : MonoBehaviour
{
    [SerializeField] private Button upButton, downButton; 
    private GameInput input;

    private void Awake()
    {
        input = new GameInput(); 
    }

    private void OnEnable()
    {
        input.Enable(); 
        input.Player.Vertical.performed += TriggerButtons; 
    }

    private void OnDisable()
    {
        input.Player.Vertical.performed -= TriggerButtons; 
        input.Disable(); 
    }

    private void TriggerButtons(InputAction.CallbackContext context)
    {
        float v = context.ReadValue<float>(); 
        if (v > 0f)
        {
            upButton.onClick.Invoke(); 
        }
        else if (v < 0f)
        {
            downButton.onClick.Invoke(); 
        }
    }
}
