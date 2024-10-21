using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Monster_ShadowBat : Monster
{
    public DarkKnight darkKnight;

    public override IEnumerator Encount()
    {
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();

        if (GameObject.Find("DarkKnight(Clone)") != null)
            darkKnight = GameObject.Find("DarkKnight(Clone)").GetComponent<DarkKnight>();
        ChangeAnimation("Idle");

        if (isDead || (darkKnight != null && darkKnight.candleN >= 2))
            StartCoroutine(Death());

        yield return null;
    }

    public override IEnumerator turnProcess()
    {
        if (isDead||(darkKnight != null && darkKnight.candleN >= 2))
        {
            StartCoroutine(Death());
            yield break;
        }
        if (summoned)
        {
            summoned = false;
            NextMove();
        }
        else
        {
            turn--;
            if (turn == 0)
            {
                yield return StartCoroutine(Active(activeNumber));
                NextMove();
            }
        }
        turnText.text = turn.ToString();
    }


    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        ChangeAnimation("Attack");
        yield return new WaitForSeconds(0.7f);
        ChangeAnimation("Idle");
        All.Manager().player.LifeChange(-2);
    }
    public override void NextMove()
    {
        activeNumber = 1;
        turnSet(Random.Range(3, 6));
    }
}
