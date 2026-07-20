using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HorizonAttackTrigger : ZoneTrigger
{
    public Sprite attackBackground;
    public GameObject backgroundObject;
    public Image overlay;
    public Checkpoint autoCheckpoint;
    public GameObject dialogueBox;
    public GameObject canvas;
    public MusicPlayer music;
    public AudioClip attackMusic;
    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        
        FindFirstObjectByType<GameManager>().EnableUIMode();
        
        autoCheckpoint.Interact();

        StartCoroutine(DoFade(0, 1, 1));
        
        music.StopPlaying();
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
        
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).name != "Overlay")
                canvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        dialogueBox.SetActive(true);
        
        GetComponent<AutoDialogue>().TriggerDialogue();
        
        yield return new WaitUntil(() => !dialogueBox.activeSelf);
        
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).name != "Overlay" || canvas.transform.GetChild(i).name != "EscapeCutscene")
                canvas.transform.GetChild(i).gameObject.SetActive(true);
        }

        canvas.transform.GetChild(0).gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        
        music.QueueSong(attackMusic, true);
        music.StartPlaying();
        
        backgroundObject.GetComponent<SpriteRenderer>().sprite = attackBackground;
        
        FindFirstObjectByType<GameManager>().EnableGameMode();
        overlay.color = new Color(1, 1, 1, 0);
    }
}
