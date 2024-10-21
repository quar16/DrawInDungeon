using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneFunc : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sprite;
    public bool blocked = false;
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        StartCoroutine(Moving());
    }
    IEnumerator Moving()
    {
        float goal = Random.Range(1f, 3f);
        if (blocked)
            goal = 5f;
        while (transform.position.y > goal)
        {
            transform.position += Vector3.down * 0.6f;
            yield return null;
        }
        for(int i = 0; i < 10; i++)
        {
            transform.localScale *= 1.01f;
            sprite.color = new Color(1, 1, 1, 1 - i / 9f);
            yield return null;
        }
        Destroy(gameObject);
    }
}
