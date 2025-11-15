using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Timer and Difficulty")] 
    public float timer;
    public TextMeshProUGUI timerText;
    
    
    [Header("Damage Indicators")]
    public GameObject damageIndicator;
    public Color playerHealColor;
    public Color enemyDamageColor;

    void Start()
    {
        timer = 0;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = time.ToString("hh\\:mm\\:ss\\.ff");
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timer);
        timerText.text = time.ToString("hh\\:mm\\:ss\\.ff");
    }
    
    void OnEnable()
    {
        EventManager.OnEnemyHit += CreatureHit;
        EventManager.OnPlayerHit += CreatureHit;
        EventManager.OnPlayerHealed += PlayerHealed;
    }

    void OnDisable()
    {
        EventManager.OnEnemyHit -= CreatureHit;
        EventManager.OnPlayerHit -= CreatureHit;
        EventManager.OnPlayerHealed -= PlayerHealed;
    }

    void CreatureHit(GameObject source, GameObject target, float amount)
    {
        var damageIndicatorInstance = Instantiate(damageIndicator);
        damageIndicatorInstance.transform.position = target.transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f));
        damageIndicatorInstance.transform.rotation = target.transform.rotation;
        damageIndicatorInstance.GetComponent<TextMeshPro>().text = amount.ToString();
        damageIndicatorInstance.GetComponent<TextMeshPro>().color = enemyDamageColor;
    }

    void PlayerHealed(GameObject player, float amount)
    {
        var damageIndicatorInstance = Instantiate(damageIndicator);
        damageIndicatorInstance.transform.position = player.transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f));
        damageIndicatorInstance.transform.rotation = player.transform.rotation;
        damageIndicatorInstance.GetComponent<TextMeshPro>().text = amount.ToString();
        damageIndicatorInstance.GetComponent<TextMeshPro>().color = playerHealColor;
    }
}
