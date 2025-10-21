using UnityEngine;
using UnityEngine.UI;

public class LocatorRotation : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        var playerPos = player.transform.position;
        var playerScreenPos = Camera.main.WorldToScreenPoint(playerPos);
        var mousePos = Input.mousePosition; ;
        var diff = mousePos - playerScreenPos;
        var rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Image>().rectTransform.localRotation = Quaternion.Euler(0f, 0f, rot - 90);
        gameObject.GetComponent<Image>().rectTransform.position = playerScreenPos;
        
        var playerController = player.GetComponent<PlayerController>();
        var rechargePercent = playerController.abilityCoolDownTimer / playerController.abilityCoolDown;
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, rechargePercent);
    }
}
