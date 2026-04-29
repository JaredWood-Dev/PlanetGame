using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LocatorRotation : MonoBehaviour
{
    public GameObject player;
    public Vector2 diff = Vector2.right;

    public TextMeshProUGUI breathText;

    public InputActionReference lookAction;
    
    void Update()
    {
        var playerPos = player.transform.position;
        var playerScreenPos = (Vector2)Camera.main.WorldToScreenPoint(playerPos);
        var mousePos = Mouse.current.position.ReadValue();
        
        if (GameManager.isController)
        { 
            if (lookAction.action.phase == InputActionPhase.Started)
                diff = lookAction.action.ReadValue<Vector2>().normalized;

            breathText.text = "RT";
        }
        else
        {
            diff = mousePos - playerScreenPos;
            
            breathText.text = "LSHIFT";
        }
            
        var rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Image>().rectTransform.localRotation = Quaternion.Euler(0f, 0f, rot - 90);
        gameObject.GetComponent<Image>().rectTransform.position = playerScreenPos;
        
        var playerController = player.GetComponent<PlayerController>();
        var rechargePercent = playerController.abilityCoolDownTimer / playerController.abilityCoolDown;
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, rechargePercent);
    }
}
