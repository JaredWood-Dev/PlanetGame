using UnityEngine;

public class AutoDialogue : MonoBehaviour
{
    //Auto Dialogue Refers to any Dialogue that is forcibly triggered, like a cut scene.
    public Dialogue dialogue;

    public bool TriggerDialogue()
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
        return true;
    }
}
