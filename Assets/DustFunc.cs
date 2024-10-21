using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustFunc : MonoBehaviour
{
    float speed;
    public SpriteRenderer sprite;
    private void Start()
    {
        sprite.sortingOrder = Random.Range(-1, 5);
        speed = Random.Range(0.04f, 0.3f);
        transform.localScale *= Random.Range(0.1f, 1f);
        float g = Random.Range(0.3f, 0.5f);
        sprite.color = new Color(g + Random.Range(0.3f, 0.5f), g, 0);
    }
    void Update()
    {
        transform.position += Vector3.left * speed;
        if (transform.position.x < -3.9f)
            Destroy(gameObject);
    }
}
