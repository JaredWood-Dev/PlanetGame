using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [Header("Ability Cell")] 
    private PlayerController _plr;
    private float _initSize;
    public Image abilityCellOverlay;


    void Start()
    {
        _plr = GetComponent<PlayerController>();
        _initSize = abilityCellOverlay.rectTransform.sizeDelta.x;
    }

    void Update()
    {
        float refillPercent = _plr.abilityCoolDownTimer / _plr.abilityCoolDown;
        abilityCellOverlay.rectTransform.sizeDelta = new Vector2(_initSize, Mathf.Min( (1 - refillPercent) * _initSize, _initSize));
    }
    
}
