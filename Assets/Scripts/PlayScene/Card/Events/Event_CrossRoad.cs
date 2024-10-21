using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_CrossRoad : Events
{
    bool trap;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        if (trap)
        {
            text.text = "앞에서 등장한 함정을 피하지 못했습니다.";
            yield return new WaitForSeconds(1);
            BG.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            All.Manager().player.LifeChange(-2);
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

    public void Select()
    {
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        trap = (Random.Range(0, 2) == 0);
        isSelected = true;
    }
}
