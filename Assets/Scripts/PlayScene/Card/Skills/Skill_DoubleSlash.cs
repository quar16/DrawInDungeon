using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DoubleSlash : Skills
{
    public override IEnumerator SkillActivating(Monster tempM)
    {
        StartCoroutine(Effect(0, tempM, 0.3f,0));
        yield return new WaitForSeconds(0.2f);
        tempM.LifeChange(All.Manager().skill.damageCalc(-2, skillType));

        yield return StartCoroutine(Effect(Random.Range(1, 3), tempM, 0.3f,1));
        if (tempM != null)
            tempM.LifeChange(All.Manager().skill.damageCalc(-2, skillType));
        yield return new WaitForSeconds(0.1f);
    }
}
