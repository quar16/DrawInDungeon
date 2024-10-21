using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelThePulse : Skills
{
    DarkKnight darkKnight;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        darkKnight = GameObject.Find("DarkKnight(Clone)").GetComponent<DarkKnight>();
        darkKnight.feelthePulse = true;
        yield break;
    }
}
