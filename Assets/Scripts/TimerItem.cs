using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "TimerItem", menuName = "Scriptable Objects/TimerItem")]
public class TimerItem : Item
{
    /*
     * A timer item activates on a specific interval.
     */
    protected GameObject player;

    public override void OnPickUp(CharacterAttributes attributes)
    {
        player = attributes.gameObject;
    }

    public override void OnRemoved(CharacterAttributes attributes)
    {
        player = null;
    }

    public virtual void OnTimerTick()
    {
       Debug.Log("TimerTick");
    }
}
