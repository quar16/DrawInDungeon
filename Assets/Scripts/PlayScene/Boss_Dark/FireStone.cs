using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStone : Skills
{
    DarkKnight darkKnight;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        darkKnight = GameObject.Find("DarkKnight(Clone)").GetComponent<DarkKnight>();
        for(int i = 0; i < 6; i++)
        {
            if (darkKnight.Candles[i].isLight)
                yield return StartCoroutine(darkKnight.Candles[i].LifeChange(3));
        }
    }
}
