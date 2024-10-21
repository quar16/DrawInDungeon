using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Chest : Events
{
    bool trap;
    bool open;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        if (open)
        {
            if (trap)
            {
                text.text = "미믹이였다!";
                yield return new WaitForSeconds(1);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                CardManager cm = All.Manager().card;
                All.Manager().monster.SummonMonster(cm.monsterPrefab[0].monster);//나중에 미믹 번호로 수정
            }
            else
            {
                text.text = "아이템을 얻었습니다.";
                yield return new WaitForSeconds(1);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                CardManager cm = All.Manager().card;
                All.Manager().item.EquipItem(cm.nowDungeonItem[Random.Range(0, cm.nowDungeonItem.Count)].item);
            }
        }
        else
        {
            text.text = "무시하고 지나갔다.";
            yield return new WaitForSeconds(1);
            BG.SetActive(false);
        }
    }

    public void Select(bool _open)
    {
        open = _open;
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        trap = (Random.Range(0, 2) == 0);
        isSelected = true;
    }
}
