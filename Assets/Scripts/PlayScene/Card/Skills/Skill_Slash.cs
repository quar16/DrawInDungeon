using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Slash : Skills
{
    public override IEnumerator SkillActivating(Monster tempM)
    {
        yield return StartCoroutine(Effect(0, tempM, 0.5f,0));
        tempM.LifeChange(All.Manager().skill.damageCalc(-2,skillType));
    }
}
