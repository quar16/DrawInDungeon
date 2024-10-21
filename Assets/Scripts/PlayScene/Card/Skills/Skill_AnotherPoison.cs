using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AnotherPoison : Skills
{
    public override IEnumerator SkillActivating(Monster tempM)
    {
        StartCoroutine(Effect(0, tempM, 0.3f,0));
        tempM.LifeChange(-6);
        if (tempM.life == 0)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Effect(1, All.Manager().player.player.transform.position + Vector3.down * 0.8f, 0.3f,1));
            All.Manager().player.LifeChange(12);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
