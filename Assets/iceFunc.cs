using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceFunc : Skills
{
    public iceSkillCard iceSkillCard;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        if (iceSkillCard != null)
            iceSkillCard.PointFunc.Unregister();
        yield break;
    }
}
