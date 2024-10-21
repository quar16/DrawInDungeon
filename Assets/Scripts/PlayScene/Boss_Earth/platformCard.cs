using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCard : SkillCard
{
    public platform platform;
    protected override IEnumerator MouseUp()
    {
        cardManager.EffectOff((int)isTargetSkill);

        Vector2 tempV2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (tempV2.y > 0 && tempV2.x > -4)
        {
            if (tempV2.y > 2.7f)
                platform.pos = 1;
            else
                platform.pos = -1;

            cardUse = true;
            All.Manager().card.TouchPrevent.SetActive(true);
            All.Manager().skill.nowSkill = platform;
            yield return StartCoroutine(platform.SkillActivating());
            yield return StartCoroutine(All.Manager().point.PointActivating(Points.ATTACK));
            cardManager.CardUse(line);
        }
    }
}
