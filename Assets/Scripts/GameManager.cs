using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Timer and Difficulty")] 
    public float timer;
    public TextMeshProUGUI timerText;
    public float difficulty;
    
    
    [Header("Damage Indicators")]
    public GameObject damageIndicator;
    public Color playerHealColor;
    public Color enemyDamageColor;
    public Color playerDamageColor;

    [Header("Stats")] 
    public int enemyKills;
    public int damageDealt;
    public int damageTaken;
    public int healing;

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

        //TODO: REPLACE WITH MORE ELEGANT SCALING SYSTEM
        difficulty = timer * 0.1f;
        //difficulty = Mathf.Pow(1.1f, 0.1f * difficulty);
    }
    
    void OnEnable()
    {
        EventManager.OnEnemyHit += CreatureHit;
        EventManager.OnPlayerHit += CreatureHit;
        EventManager.OnPlayerHealed += PlayerHealed;
        EventManager.OnEnemyDied += EnemyKilled;
    }

    void OnDisable()
    {
        EventManager.OnEnemyHit -= CreatureHit;
        EventManager.OnPlayerHit -= CreatureHit;
        EventManager.OnPlayerHealed -= PlayerHealed;
        EventManager.OnEnemyDied -= EnemyKilled;
    }

    void CreatureHit(GameObject source, GameObject target, float amount)
    {
        var damageIndicatorInstance = Instantiate(damageIndicator);
        damageIndicatorInstance.transform.position = target.transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f));
        damageIndicatorInstance.transform.rotation = target.transform.rotation;
        damageIndicatorInstance.GetComponent<TextMeshPro>().text = amount.ToString();
        if (target.CompareTag("Player"))
        {
            damageIndicatorInstance.GetComponent<TextMeshPro>().color = playerDamageColor;
            damageTaken += (int)amount;
        }
        else
        {
            damageIndicatorInstance.GetComponent<TextMeshPro>().color = enemyDamageColor;
            damageDealt += (int)amount;
        }
    }

    void PlayerHealed(GameObject player, float amount)
    {
        var damageIndicatorInstance = Instantiate(damageIndicator);
        damageIndicatorInstance.transform.position = player.transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f));
        damageIndicatorInstance.transform.rotation = player.transform.rotation;
        damageIndicatorInstance.GetComponent<TextMeshPro>().text = amount.ToString();
        damageIndicatorInstance.GetComponent<TextMeshPro>().color = playerHealColor;
        
        healing += (int)amount;
    }

    void EnemyKilled(GameObject target, GameObject killer)
    {
        enemyKills++;
        if (enemyKills >= 10)
        {
            //Reload the scene once you get enough kills
            SceneManager.LoadScene(0);
        }
    }
}
