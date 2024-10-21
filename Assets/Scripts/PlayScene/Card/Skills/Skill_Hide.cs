using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hide : Skills
{
    public PointFunc pointFunc;
    public override IEnumerator SkillActivating(Monster tempM)
    {
        yield return StartCoroutine(Effect(0, All.Manager().player.player.transform.position + Vector3.down * 1.7f, 0,0));
        All.Manager().player.player.color = new Color(1, 1, 1, 0.5f);
        Instantiate(pointFunc, Vector3.up * 100, Quaternion.identity).Register();
        All.Manager().player.alleviation = 0;
    }
}
