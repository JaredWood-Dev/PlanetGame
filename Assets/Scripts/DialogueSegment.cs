using UnityEngine;

[System.Serializable]
public class DialogueSegment
{
    public Speaker CurrentSpeaker;
    public bool displaySpeaker;
    [TextArea(3, 10)]
    public string Sentence;
}
