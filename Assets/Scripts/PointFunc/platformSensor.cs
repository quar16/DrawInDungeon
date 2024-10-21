using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSensor : PointFunc
{
    public override IEnumerator CouFunc()
    {
        if (All.Manager().skill.nowSkill.skillType == SkillType.AP || All.Manager().skill.nowSkill.skillType == SkillType.AD)
        {

            EarthboundGolem earthboundGolem;
            earthboundGolem = GameObject.Find("EarthboundGolem(Clone)").GetComponent<EarthboundGolem>();
            earthboundGolem.playerPos = 0;
            earthboundGolem.platformObj.transform.position = new Vector3(-1.7f, 100, 0);
            All.Manager().player.player.transform.position = new Vector3(-1.7f, 2.4f, 0);
            yield return null;
        }
    }
}
