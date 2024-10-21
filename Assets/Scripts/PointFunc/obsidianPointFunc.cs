using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsidianPointFunc : PointFunc
{
    public int AP_Bouns;
    
    public GameObject effect;
    public AudioClip clip;
    public override void Register()
    {
        All.Manager().skill.stat_AP += AP_Bouns;
        base.Register();
    }
    public override void Unregister()
    {
        All.Manager().skill.stat_AP -= AP_Bouns;
        base.Unregister();
    }

    public override IEnumerator CouFunc()
    {
        if(All.Manager().skill.nowSkill.skillType == SkillType.AP)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 3; i++)
            {
                if (All.Manager().monster.nowMonsters[i] != null)
                {
                    yield return new WaitForSeconds(0.05f);
                    All.EffectSound(clip);
                    Instantiate(effect, All.Manager().monster.nowMonsters[i].EffectTarget.transform.position, Quaternion.identity);
                    All.Manager().monster.nowMonsters[i].LifeChange(-3);
                }
            }
        }
    }
}
