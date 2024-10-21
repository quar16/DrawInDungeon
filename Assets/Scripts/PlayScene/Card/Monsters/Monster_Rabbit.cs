using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Rabbit : Monster
{
    int target;
    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        switch (active)
        {
            default:
            case 0:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                All.Manager().player.LifeChange(-1);
                ChangeAnimation("Idle");
                break;
            case 1:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                if (All.Manager().monster.nowMonsters[target] != null)
                    All.Manager().monster.nowMonsters[target].LifeChange(15);
                ChangeAnimation("Idle");
                break;
        }
    }
    public override void NextMove()
    {
        bool check = false;
        for (int i = 0; i < 3; i++)
        {
            Monster tempM = All.Manager().monster.nowMonsters[i];
            if (tempM != null && tempM.MaxLife - tempM.life > 5)
            {
                check = true;
                target = i;
                activeNumber = 1;
                turnSet(2);

                if (Random.Range(0, 3) == 0)
                    break;
            }
        }
        if (!check)
        {
            activeNumber = 0;
            turnSet(3);
        }
    }
}
