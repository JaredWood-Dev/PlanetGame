using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Speaker", menuName = "Scriptable Objects/Speaker")]
public class Speaker : ScriptableObject
{
    /*
     * The speaker object contains information about who is speaking in dialogue.
     * It contains the Name, and a Portrait.
     */

    public string Name;
    public Sprite Portrait;
}
