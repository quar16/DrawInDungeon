using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_blade : Events
{
    public PointFunc PointFunc;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);

        text.text = "이제 당신의 검들은 모두 한단계 더 강해진 모습을 뽑낼겁니다.\n잠시동안은말이죠.\n\n5턴동안 공격력 +5";
        yield return new WaitUntil(() => NextMoveCheck);
        BG.SetActive(false);
        Instantiate(PointFunc).Register();
    }

    public void Select()
    {
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        isSelected = true;
        checkButton.SetActive(true);
    }
}
