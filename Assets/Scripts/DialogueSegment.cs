using UnityEngine;

[System.Serializable]
public class DialogueSegment
{
    public Speaker CurrentSpeaker;
    [TextArea(3, 10)]
    public string Sentence;
}
