using System.Collections;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;
    public CameraFunctions cameraFunctions;
    
    public override void Interact()
    {
        isClose = false;

        StartCoroutine(InteractSequence());
    }

    IEnumerator InteractSequence()
    {
        StartCoroutine(cameraFunctions.Zoom(5, 0.5f));

        yield return new WaitForSeconds(0.5f);
        
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);

        cameraFunctions.ResetZoom();
    }
}
