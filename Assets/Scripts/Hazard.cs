using Unity.Cinemachine;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Armor>().Hit(1, gameObject, ((Vector2)collision.gameObject.transform.position - collision.GetContact(0).point) * 2500);
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }
}
