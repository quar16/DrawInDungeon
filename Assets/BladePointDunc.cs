using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladePointDunc : PointFunc
{
    public override void Register()
    {
        All.Manager().skill.stat_AD += 5;
        base.Register();
    }
    int turn = 6;
    public override IEnumerator CouFunc()
    {
        turn--;
        if (turn == 0)
        {
            All.Manager().skill.stat_AD -= 5;
            StartCoroutine(Unregist());
            yield break;
        }
    }

    IEnumerator Unregist()
    {
        yield return new WaitUntil(() => All.Manager().point.PointProcessEnd);
        Unregister();
        Destroy(gameObject);
    }
}
