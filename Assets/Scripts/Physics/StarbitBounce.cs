using System.Collections.Generic;
using UnityEngine;

public class StarbitBounce : MonoBehaviour
{
    public float bounceHeight;
    public float rotationSpeed;
    private GameObject _child;
    private float _rotationAngle = 0;
    private int _rotationDirection = 0;
    public bool randomColors;
    public Color chosenColor;
    public List<Color> colors;
    public bool isStatic = false;

    void Start()
    {
        _child = transform.GetChild(0).gameObject;
        _rotationDirection = Random.Range(0, 2) * 2 - 1; 
        
        if (randomColors)
            _child.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];
        else
            _child.GetComponent<SpriteRenderer>().color = chosenColor;

        if (isStatic)
            GetComponent<GravityObject>().gravity = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * bounceHeight, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        _rotationAngle += rotationSpeed * Time.fixedDeltaTime * _rotationDirection;
        _child.transform.rotation = Quaternion.Euler(0f, 0f, _rotationAngle);
    }
}
