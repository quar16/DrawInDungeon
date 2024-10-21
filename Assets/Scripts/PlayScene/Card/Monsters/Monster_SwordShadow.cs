using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_SwordShadow : Monster
{
    public GameObject hideEffect;
    bool hide = false;
    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        switch (activeNumber)
        {
            case 0:
                ChangeAnimation("Walk");
                yield return new WaitForSeconds(0.4f);
                hide = true;
                hideEffect.SetActive(true);
                ChangeAnimation("Idle");
                break;

            case 1:
                hide = false;
                hideEffect.SetActive(false);
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                All.Manager().player.LifeChange(-6);
                ChangeAnimation("Idle");
                break;
        }
    }


    public override void LifeChange(int damage)
    {
        if (hide)
            damage = 0;

        StartCoroutine(All.Manager().monster.damageTextMoving(damage, transform.position.x));
        life = Mathf.Clamp(life + damage, 0, MaxLife);
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();
        LifeGauge.localScale = new Vector2((float)life / MaxLife, LifeGauge.localScale.y);

        if (life == 0)
            isDead = true;
    }

    public bool first = true;
    public override void NextMove()
    {
        if (first)
        {
            activeNumber = 0;
            turnSet(1);
            first= false;
        }
        else
        {
            activeNumber = 1;
            turnSet(1);
            first = true;
        }
    }
}
