using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NabiPointFunc : PointFunc
{
    public int AD_Bouns;
    public int turn = 3;
    public AudioClip clip;
    public override void Register()
    {
        All.Manager().skill.stat_AD += AD_Bouns;
        turn = 3;
        base.Register();
    }
    public override void Unregister()
    {
        All.Manager().skill.stat_AD -= AD_Bouns;
        base.Unregister();
    }
    public GameObject effect;
    public override IEnumerator CouFunc()
    {
        if (turn == 0 && All.Manager().skill.nowSkill.skillType == SkillType.AD)
        {
            if (All.Manager().skill.nowMonster != null)
            {
                yield return new WaitForSeconds(0.1f);
                Instantiate(effect, All.Manager().skill.nowMonster.EffectTarget.transform.position, Quaternion.identity);
                All.EffectSound(clip);

                yield return new WaitForSeconds(0.1f);
                All.Manager().skill.nowMonster.LifeChange(-7);
                turn = 4;
            }
        }
    }
}
