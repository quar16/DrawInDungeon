using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FireDragon : Monster
{
    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        switch (active)
        {
            default:
            case 0:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                All.Manager().player.LifeChange(-5);
                ChangeAnimation("Idle");
                break;
            case 1:
                ChangeAnimation("Attack");
                for (int i = 0; i < 7; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    All.Manager().player.LifeChange(-2);
                }
                ChangeAnimation("Idle");
                break;
        }
    }
    public override void NextMove()
    {
        if (Random.Range(0, 4) != 0)
        {
            activeNumber = 0;
            turnSet(2);
        }
        else
        {
            activeNumber = 1;
            turnSet(4);
        }
    }
}
