using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ice : Skills
{
    public override IEnumerator SkillActivating(Monster tempM)
    {
        yield return StartCoroutine(Effect(0,tempM, 0.4f,0));
        tempM.LifeChange(All.Manager().skill.damageCalc(-3,skillType));
        tempM.turn += 2;
        tempM.turnText.text = tempM.turn.ToString();
    }
}
