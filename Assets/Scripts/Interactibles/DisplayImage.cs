using UnityEngine;

public class DisplayImage : MonoBehaviour
{
    public Sprite image;
    private SpriteRenderer _sr;

    void Start()
    {
        _sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        _sr.sprite = image;
    }
    
}
