using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RuleOfDesert : Skills
{
    public PointFunc pointFunc;
    public override IEnumerator SkillActivating(Monster tempM)
    {
        yield return StartCoroutine(Effect(0, new Vector3(3, 4, 0), 0.4f,0));
        for (int j = 0; j < 3; j++)
        {
            Monster tempM2 = All.Manager().monster.nowMonsters[j];
            if (tempM2 != null)
            {
                tempM2.LifeChange(All.Manager().skill.damageCalc(-10, skillType));
            }
        }
        All.Manager().player.alleviation /= 2;
        Instantiate(pointFunc, Vector3.up * 100, Quaternion.identity).Register();
    }
}
