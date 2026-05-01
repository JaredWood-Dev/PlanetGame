using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    
    public override void Interact()
    {
        isClose = false;
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
