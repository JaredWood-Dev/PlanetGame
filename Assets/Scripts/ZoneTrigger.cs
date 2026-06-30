using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Trigger");
        }
    }
}
