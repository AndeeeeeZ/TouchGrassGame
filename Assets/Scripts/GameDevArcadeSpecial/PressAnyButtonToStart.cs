using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PressAnyButtonToStart : MonoBehaviour
{
    [SerializeField] private Button button;
    private GameInput input;

    private void Awake()
    {
        input = new GameInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.ArcadeAnyButton.performed += TriggerButton;
    }

    private void OnDisable()
    {
        input.Player.ArcadeAnyButton.performed -= TriggerButton;
        input.Disable();
    }

    private void TriggerButton(InputAction.CallbackContext context)
    {
        button.onClick.Invoke();
    }
}
