using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HorizonAttackTrigger : ZoneTrigger
{
    public Sprite attackBackground;
    public GameObject backgroundObject;
    public Image overlay;
    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        
        FindFirstObjectByType<GameManager>().EnableUIMode();

        StartCoroutine(DoFade(0, 1, 1));
    }

    IEnumerator DoFade(float start, float end, float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, counter / duration);
            overlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
       
        yield return new WaitForSeconds(0.5f);
        
        backgroundObject.GetComponent<SpriteRenderer>().sprite = attackBackground;
        
        FindFirstObjectByType<GameManager>().EnableGameMode();
        overlay.color = new Color(1, 1, 1, 0);
    }
}
