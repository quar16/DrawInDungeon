using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_secretCall : Events
{
    int option;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        switch (option)
        {
            case 0:
                text.text = "\"이게 너의 힘이 되어줄지도 모르지, 최선을 다해보게 친구\"\n무작위 스킬카드를 두장 얻습니다"; 
                yield return new WaitUntil(() => NextMoveCheck);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);

                SkillCard[] skillPrefab = Resources.LoadAll<SkillCard>("Skills");

                for (int i = 0; i < 2; i++)
                    yield return StartCoroutine(All.Manager().card.CardAdd(Rnd(), skillPrefab[Random.Range(0, skillPrefab.Length)]));
                break;
            case 1:
                text.text = "\"하! 건방지군. 나의 힘을 조금만 보여주도록하지!\"\n모든 캐릭터에게 20의 피해";
                yield return new WaitUntil(() => NextMoveCheck);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                All.Manager().player.LifeChange(-20);
                for (int j = 0; j < 3; j++)
                {
                    Monster tempM2 = All.Manager().monster.nowMonsters[j];
                    if (tempM2 != null)
                    {
                        tempM2.LifeChange(-20);
                    }
                }
                break;
        }
    }

    int Rnd()
    {
        while (true)
        {
            int r = Random.Range(0, 7);
            if (r != lineN)
                return r;
        }
    }
    public void Select(int _option)
    {
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        isSelected = true;
        option = _option;
        checkButton.SetActive(true);
    }
}
