using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image speakerImage;
    public Image speakerFrame;
    public UnityEngine.UI.Button speakerButton;
    
    private Queue<DialogueSegment> _dialogueSegments;

    void Start()
    {
        dialogueBox.SetActive(false);
        _dialogueSegments = new Queue<DialogueSegment>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _dialogueSegments.Clear();
        
        dialogueBox.SetActive(true);
        foreach (var sentance in dialogue.Sentences)
        {
            _dialogueSegments.Enqueue(sentance);
        }

        speakerButton.Select();
        FindFirstObjectByType<GameManager>().EnableUIMode();
        Time.timeScale = 0;
        NextSentence();
    }

    public void NextSentence()
    {
        if (_dialogueSegments.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        DialogueSegment sentence = _dialogueSegments.Dequeue();
        print(sentence.CurrentSpeaker + " said: " + sentence.Sentence);

        if (sentence.displaySpeaker)
        {
            speakerFrame.gameObject.SetActive(true);
            nameText.gameObject.SetActive(true);
            speakerImage.gameObject.SetActive(true);
            nameText.text = sentence.CurrentSpeaker.Name;
            speakerImage.sprite = sentence.CurrentSpeaker.Portrait;
        }
        else
        {
            speakerFrame.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
            speakerImage.gameObject.SetActive(false);
        }
        
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.Sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        
    }

    public void EndDialogue()
    {
        FindFirstObjectByType<GameManager>().EnableGameMode();
        dialogueBox.SetActive(false);
        Time.timeScale = 1;
        print("End of Dialogue");
    }
}
