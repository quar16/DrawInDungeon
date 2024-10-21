using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkForce : Skills
{
    DarkKnight darkKnight;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        darkKnight = GameObject.Find("DarkKnight(Clone)").GetComponent<DarkKnight>();
        darkKnight.darkForceN++;
        darkKnight.DarkForceText.text = darkKnight.darkForceN.ToString();
        yield break;
    }
}
