using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_guard : Skills
{
    public override IEnumerator SkillActivating(Monster tempM)
    {
        yield return StartCoroutine(Effect(0, new Vector3(-1, 2, 0), 0.5f,0));
        All.Manager().player.DefendChange(5);
        yield return new WaitForSeconds(0.2f);
    }
}
