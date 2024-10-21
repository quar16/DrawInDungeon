using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FloatingRock : Monster
{
    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        ChangeAnimation("Attack");
        yield return new WaitForSeconds(0.7f);
        All.Manager().player.LifeChange(-2);
        ChangeAnimation("Idle");
    }
    public override void NextMove()
    {
        activeNumber = 1;
        turnSet(2);
    }
}
