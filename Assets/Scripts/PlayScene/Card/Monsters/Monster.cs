using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int activeNumber = -1;
    public int turn = 0;
    public int MaxLife;
    public int life;
    public TextMeshPro turnText;
    public Transform LifeGauge;
    public TextMeshPro LifeText;
    public int pos;
    public bool isSelected = false;

    public bool summoned = true;
    public virtual IEnumerator turnProcess()
    {
        if (isDead)
        {
            if (All.Manager().monster.MonsterNumber == 1)
                yield return StartCoroutine(Death());
            else
                StartCoroutine(Death());
            yield break;
        }
        if (summoned)
        {
            summoned = false;
            yield break;
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
        yield return StartCoroutine(TurnTextChanging());
    }

    protected IEnumerator TurnTextChanging()
    {
        turnText.text = turn.ToString();
        turnText.color = Color.clear;
        Vector2 tempV = turnText.GetComponent<RectTransform>().localScale;
        turnText.GetComponent<RectTransform>().localScale *= 2;
        float max = 10;
        for(int i = 0; i <= max; i++)
        {
            yield return null;
            turnText.color = new Color(1, 1, 1, i / max);
            turnText.GetComponent<RectTransform>().localScale = tempV * (2 - i / max);
        }
    }

    private void OnMouseEnter()
    {
        isSelected = true;
    }
    
    private void OnMouseExit()
    {
        isSelected = false;
    }
    public virtual void LifeChange(int damage)
    {
        StartCoroutine(All.Manager().monster.damageTextMoving(damage, transform.position.x));
        life = Mathf.Clamp(life + damage, 0, MaxLife);
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();
        LifeGauge.localScale = new Vector2((float)life / MaxLife, LifeGauge.localScale.y);

        if (life == 0)
            isDead = true;
    }

    public bool isDead = false;
    public virtual IEnumerator Death()
    {
        All.Manager().monster.nowMonsters[pos] = null;
        ChangeAnimation("Dead");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void turnSet(int i)
    {
        turn = i;
        turnText.text = turn.ToString();
    }

    public virtual IEnumerator Encount()
    {
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();
        ChangeAnimation("Idle");
        //yield return 
        StartCoroutine(TurnTextChanging());
        NextMove();
        yield return null;
    }

    public virtual IEnumerator Active(int active)//switch문으로 행동 조절
    {
        yield return null;
    }
    public virtual void NextMove()//switch문으로 행동 조절
    {

    }
    public GameObject EffectTarget;
    public SkeletonAnimation monsterAnimator; //The animator script of the monster
    public void ChangeAnimation(string AnimationName)  //Names are: Idle, Walk, Dead and Attack
    {
        if (monsterAnimator == null)
            return;

        bool IsLoop = true;
        if (AnimationName == "Dead")
            IsLoop = false;

        //set the animation state to the selected one
        monsterAnimator.AnimationState.SetAnimation(0, AnimationName, IsLoop);
    }
}
