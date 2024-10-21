using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteFlowerPointFunc : PointFunc
{
    int turn = 10;
    public GameObject effect;
    public AudioClip clip;
    public override IEnumerator CouFunc()
    {
        turn--;
        if (turn == 0)
        {
            yield return new WaitForSeconds(0.1f);
            turn = 10;
            All.EffectSound(clip);
            Instantiate(effect, All.Manager().player.player.transform.position + Vector3.down * 0.8f, Quaternion.identity);

            All.Manager().player.LifeChange(10);
        }
    }
}
