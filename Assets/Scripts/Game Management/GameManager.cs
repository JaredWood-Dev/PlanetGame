using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Indicates whether the player is using a gamepad or a keyboard
    [SerializeField]
    public static bool isController = false;


    private void LateUpdate()
    {
        var gamepad = Gamepad.current.lastUpdateTime;
        var keyboard = Keyboard.current.lastUpdateTime;
        isController = (gamepad > keyboard) ? true : false;
        
    }
}
