using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveFunc : MonoBehaviour
{
    public SpriteRenderer sprite;
    public int dir;
    void Start()
    {
        StartCoroutine(Acting());
    }

    IEnumerator Acting()
    {
        for (int i = 0; i <= 30; i++)
        {
            if (dir == -1)
                if (i % 2 == 0 && i < 20)
                    All.Manager().player.player.transform.position += new Vector3((i / 2 % 2) * 0.06f - 0.03f, 0, 0);
            
            //sprite.color = new Color(1, 1, 1, Mathf.Pow(1 - i / 30f, 2));
            sprite.color = new Color(1, 1, 1, (1 - i / 30f));
            transform.position += Vector3.right * 0.01f * dir;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        All.Manager().player.player.sprite = All.Manager().player.playerImages[0];
        Destroy(gameObject);
    }
}
