using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [Header("Ability Cell")] 
    private PlayerController _plr;
    private float _initSize;
    public Image abilityCellOverlay;
    private Health _hlth;
    public Image healthBar;
    private float _initHealth;
    public TextMeshProUGUI healthText;


    void Start()
    {
        _plr = GetComponent<PlayerController>();
        _hlth = GetComponent<Health>();
        _initSize = abilityCellOverlay.rectTransform.sizeDelta.x;
        _initHealth = healthBar.rectTransform.sizeDelta.x;
    }

    void Update()
    {
        float refillPercent = _plr.abilityCoolDownTimer / _plr.abilityCoolDown;
        abilityCellOverlay.rectTransform.sizeDelta = new Vector2(_initSize, Mathf.Min( (1 - refillPercent) * _initSize, _initSize));
        float healthfill = _hlth.currentHealth / _hlth.maxHealth;
        healthBar.rectTransform.sizeDelta = new Vector2(Mathf.Min(healthfill * _initHealth, _initHealth), healthBar.rectTransform.sizeDelta.y);
        healthText.text = _hlth.currentHealth + "/" + _hlth.maxHealth;
    }
    
}
