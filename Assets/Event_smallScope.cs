using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_smallScope : Events
{
    bool use;
    string[] ele = { 
        "<color=red>용암이 들끓는 화산지대</color>의", 
        "<color=aqua>얼어붙은 바다</color>의", 
        "<color=lime>거센 바람이 부는 협곡</color>의", 
        "<color=maroon>끝없는 지하와 거대한 바위산</color>의", 
        "<color=grey>칠흑같은 어둠의 땅과 하늘</color>의", 
        "<color=yellow>새하얀 빛의 땅과 하늘</color>의" };
    string[] ele2 = {
        "<color=red>화</color>",
        "<color=aqua>수</color>",
        "<color=lime>풍</color>",
        "<color=maroon>지</color>",
        "<color=grey>암</color>",
        "<color=yellow>명</color>"  };
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        if (use)
        {
            int index = 0;

            int r = Random.Range(0, 6);
            for (int i = 0; i < 6; i++)
            {
                if (!All.Manager().dungeon.elements[(i + r) % 6])
                {
                    All.Manager().dungeon.elements[(i + r) % 6] = true;
                    index = (i + r) % 6;
                    break;
                }
            }

            text.text = "망원경을 통해 앞을 바라보자 당신의 눈앞에 " + ele[index] + " 풍경이 펼쳐졌습니다. \n깜짝놀라 망원경에서 눈을 땐 당신 앞에는 여전히 어두운 동굴 뿐입니다.\n 던전에 "+ele2[index]+"속성이 추가됩니다."; 
            yield return new WaitUntil(() => NextMoveCheck);

            BG.SetActive(false);

            All.Manager().dungeon.NowDungeonSet();
        }
        else
        {
            text.text = "무시하고 지나갔다."; 
            yield return new WaitUntil(() => NextMoveCheck);
            BG.SetActive(false);
        }
    }

    public void Select(bool _use)
    {
        use = _use;
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        isSelected = true;
        checkButton.SetActive(true);
    }
}
