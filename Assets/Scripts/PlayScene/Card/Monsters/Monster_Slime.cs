using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Slime : Monster
{
    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        ChangeAnimation("Attack");
        yield return new WaitForSeconds(0.7f);
        All.Manager().player.LifeChange(-1);
        ChangeAnimation("Idle");
    }
    public override void NextMove()
    {
        turnSet(Random.Range(1, 4));
    }
}
