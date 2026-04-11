using UnityEngine;
using UnityEngine.InputSystem;

public class ExitApplication : MonoBehaviour
{
    private GameInput input;

    private void Awake()
    {
        input = new GameInput(); 
    }

    private void OnEnable()
    {
        input.Enable(); 
        input.Player.Quit.performed += quitApp; 
    }

    private void OnDisable()
    {
        input.Player.Quit.performed -= quitApp; 
        input.Disable(); 
    }

    private void quitApp(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
