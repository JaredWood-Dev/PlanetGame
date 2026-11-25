using UnityEngine;

public class Collectible : MonoBehaviour
{
    // An object that has this component can be collected by the player.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           Collect();
        }
    }

    public virtual void Collect()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
