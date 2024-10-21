using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public enum TargetType { TARGET, NONETARGET, PLATFORM }
public class SkillCard : Card
{
    public Skills skills;
    public TargetType isTargetSkill;

    private void OnMouseDown()
    {
        if (isFront && !cardUse)
        {
            cardManager.EffectOn((int)isTargetSkill);//나중에는 카드마다 판정이 바뀔 예정
        }
    }
    private void OnMouseDrag()
    {
        if (isFront && !cardUse)
        {
            cardManager.EffectMove();//나중에는 카드마다 판정이 바뀔 예정
        }
    }

    protected override IEnumerator MouseUp()
    {
        cardManager.EffectOff((int)isTargetSkill);
        if (isFront)
        {
            if (All.Manager().card.Dispose)
            {
                cardUse = true;
                All.Manager().card.TouchPrevent.SetActive(true);
                cardManager.CardUse(line);
            }

            if (isTargetSkill == 0)
            {
                Monster tempM = All.Manager().monster.target();
                All.Manager().skill.nowMonster = tempM;
                if (tempM != null)
                {
                    cardUse = true;
                    All.Manager().player.PlayerMoving(1);
                    All.Manager().card.TouchPrevent.SetActive(true);
                    All.Manager().skill.nowSkill = skills;
                    yield return StartCoroutine(skills.SkillActivating(tempM));
                    yield return StartCoroutine(All.Manager().point.PointActivating(Points.ATTACK));
                    cardManager.CardUse(line);
                }
            }
            else
            {
                Vector2 tempV2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (tempV2.y > 0 && tempV2.x > -4)
                {
                    cardUse = true;
                    All.Manager().player.PlayerMoving(1);
                    All.Manager().card.TouchPrevent.SetActive(true);
                    All.Manager().skill.nowSkill = skills;
                    yield return StartCoroutine(skills.SkillActivating());
                    yield return StartCoroutine(All.Manager().point.PointActivating(Points.ATTACK));
                    cardManager.CardUse(line);
                }
            }
        }
    }
}

