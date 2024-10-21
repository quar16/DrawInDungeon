using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NabiFlowPointFunc : PointFunc
{
    public NabiPointFunc nabi;
    public GameObject ChargeEffect;
    public override IEnumerator CouFunc()
    {
        if (nabi.turn > 0)
            nabi.turn--;
        if (nabi.turn == 0)
            ChargeEffect.SetActive(true);
        else
            ChargeEffect.SetActive(false);
        yield break;
    }
}
