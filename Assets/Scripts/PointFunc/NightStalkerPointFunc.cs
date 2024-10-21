using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightStalkerPointFunc : PointFunc
{
    public int AP_Bouns;
    public GameObject effect;
    public override void Register()
    {
        All.Manager().skill.stat_AP += AP_Bouns;
        base.Register();
    }
    public override void Unregister()
    {
        All.Manager().skill.stat_AP -= AP_Bouns;
        base.Unregister();
    }

    public int turn = 0;
    public override IEnumerator CouFunc()
    {
        if (turn != 0)
        {
            turn--;
            if (turn == 0)
                All.Manager().skill.stat_AP += AP_Bouns;

            effect.SetActive(false);
        }
        else
        {
            effect.SetActive(true);
        }
        yield break;
    }
}
