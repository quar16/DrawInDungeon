using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_HealingPond : Events
{
    int option;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        switch (option)
        {
            case 0:
                text.text = "연못의 신비한 빛이 몸을 감쌉니다. \n 체력 +3 최대 체력 +5";
                yield return new WaitForSeconds(1);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                All.Manager().player.LifeChange(3, 5);
                break;
            case 1:
                text.text = "시원한 느낌이 온몸을 타고 흐릅니다. \n 체력 전부 회복";
                yield return new WaitForSeconds(1);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                All.Manager().player.LifeChange(999);
                break;
        }
    }

    public void Select(int _option)
    {
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        isSelected = true;
        option = _option;
    }
}
