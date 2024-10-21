using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Yeti : Monster
{
    public Card ice;
    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        switch (active)
        {
            default:
            case 0:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                All.Manager().player.LifeChange(-15);
                ChangeAnimation("Idle");
                break;
            case 1:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.5f);
                
                for (int i = 0; i < 3; i++)
                    yield return StartCoroutine(All.Manager().card.CardAdd(Random.Range(0, 7), ice));
                
                ChangeAnimation("Idle");
                break;
        }
    }
    public override void NextMove()
    {
        if (Random.Range(0, 2) == 0)
        {
            activeNumber = 0;
            turnSet(5);
        }
        else
        {
            activeNumber = 1;
            turnSet(1);
        }
    }
}
