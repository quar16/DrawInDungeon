using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Prophecy : Skills
{
    public PointFunc pointFunc;
    public override IEnumerator SkillActivating(Monster tempM)
    {
        Vector3 basePoint = All.Manager().player.player.transform.position;
        yield return StartCoroutine(Effect(0, basePoint + new Vector3(0.5f, 1.5f, 0), 0,0));
        Instantiate(pointFunc, Vector3.up * 100, Quaternion.identity).Register();
    }
}
