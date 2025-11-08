using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject damageIndicator;
    
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
    }

    void PlayerHealed(GameObject player, float amount)
    {
        var damageIndicatorInstance = Instantiate(damageIndicator);
        damageIndicatorInstance.transform.position = player.transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f));
        damageIndicatorInstance.transform.rotation = player.transform.rotation;
        damageIndicatorInstance.GetComponent<TextMeshPro>().text = amount.ToString();
        damageIndicatorInstance.GetComponent<TextMeshPro>().color = Color.green;
    }
}
