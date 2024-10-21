using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FlareDrive : Skills
{
    public override IEnumerator SkillActivating(Monster tempM)
    {
        tempM.LifeChange(All.Manager().skill.damageCalc(-5, skillType));
        yield return StartCoroutine(Effect(0, tempM, 0.4f,0));
        for (int i = 0; i < 3; i++)
        {
            int r = Random.Range(0, 3);
            for (int j = 0; j < 3; j++)
            {
                Monster tempM2 = All.Manager().monster.nowMonsters[(r + j) % 3];
                if (tempM2 != null && tempM2.life != 0)
                {
                    tempM2.LifeChange(All.Manager().skill.damageCalc(-5, skillType));
                    yield return StartCoroutine(Effect(0, tempM2, 0.4f,0));
                    break;
                }
            }
        }
    }
}
