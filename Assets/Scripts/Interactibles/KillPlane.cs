using UnityEngine;

public class KillPlane : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Armor>())
            other.GetComponent<Armor>().DestroyCreature();
    }
}
