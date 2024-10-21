using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePoint : PointFunc
{
    public override IEnumerator CouFunc()
    {
        All.Manager().player.alleviation = 1;
        All.Manager().player.player.color = new Color(1, 1, 1, 1);
        StartCoroutine(Unregist());
        yield break;
    }
    IEnumerator Unregist()
    {
        yield return new WaitUntil(() => All.Manager().point.PointProcessEnd);
        Unregister();
        Destroy(gameObject);
    }
}
