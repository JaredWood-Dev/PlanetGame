using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Indicates whether the player is using a gamepad or a keyboard
    [SerializeField]
    public static bool isController = false;
    
    [SerializeField]
    private PlayerInput playerInput;

    public Vector2 checkpointPosition;
    public GameObject player;
    public int starbits;
    public TMP_Text starbitCounter;


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

    public void ResetToCheckpoint()
    {
        player.transform.position = new Vector3(checkpointPosition.x, checkpointPosition.y, -1);
        player.GetComponent<Armor>().RestoreArmor(100);
    }

    public void UpdateCheckpoint(Vector2 position)
    {
        checkpointPosition = position;
    }

    void OnEnable()
    {
        EventManager.OnPlayerDeath += ResetToCheckpoint;
        EventManager.OnCheckPointUpdate += UpdateCheckpoint;
    }

    void OnDisable()
    {
        EventManager.OnPlayerDeath -= ResetToCheckpoint;
        EventManager.OnCheckPointUpdate -= UpdateCheckpoint;
    }
}
