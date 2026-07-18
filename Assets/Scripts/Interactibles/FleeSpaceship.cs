using System.Collections;
using UnityEngine;

public class FleeSpaceship : Interactable
{
    public GameObject cutscene;
    public GameObject spaceship;
    public GameObject canvas;

    public override void Interact()
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).name != "Overlay")
                canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        cutscene.SetActive(true);

        StartCoroutine(nameof(moveSpaceship), 0.01f);
    }

    IEnumerator moveSpaceship(float speed)
    {
        var rectTransform = spaceship.GetComponent<RectTransform>();
        for (int i = 0; i < 1000; i++)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + i * speed, rectTransform.anchoredPosition.y);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.1f); 
        
        GetComponent<AutoDialogue>().TriggerDialogue();
    }
}
