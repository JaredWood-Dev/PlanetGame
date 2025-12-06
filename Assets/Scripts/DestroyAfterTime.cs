using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float time;
    void Start()
    {
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
