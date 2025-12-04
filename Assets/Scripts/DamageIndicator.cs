using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public float scaleIncrease;
    public Vector2 positionOffset;
    public float lifespan;
    private Vector2 _originalPos;

    void Start()
    {
        Invoke("DestroyIndicator", lifespan);
        _originalPos = transform.position;
    }
    void Update()
    {
        transform.position = Vector2.Lerp(_originalPos, (Vector2)transform.position + positionOffset, Time.deltaTime);
        transform.rotation = Camera.main.transform.rotation;
    }

    void DestroyIndicator()
    {
        Destroy(gameObject);
    }
}
