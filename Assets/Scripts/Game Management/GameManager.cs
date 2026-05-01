using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Indicates whether the player is using a gamepad or a keyboard
    [SerializeField]
    public static bool isController = false;
    
    [SerializeField]
    private PlayerInput playerInput;


    private void LateUpdate()
    {
        var gamepad = 0.0;
        var keyboard = 0.0;

        if (Gamepad.current != null)
            gamepad = Gamepad.current.lastUpdateTime;
        keyboard = Keyboard.current.lastUpdateTime;
        isController = (gamepad > keyboard) ? true : false;
        
    }

    public void EnableUIMode()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }

    public void EnableGameMode()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
}
