using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : Skills
{
    DarkKnight darkKnight;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        darkKnight = GameObject.Find("DarkKnight(Clone)").GetComponent<DarkKnight>();
        yield return StartCoroutine(darkKnight.CandleActivate());
    }
}