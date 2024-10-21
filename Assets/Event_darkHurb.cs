using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_darkHurb : Events
{
    int option;
    public Card hide;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        switch (option)
        {
            case 0:
                text.text = "다행이 이상한 일은 일어나지 않은 듯 합니다. 몸이 가벼워 지는게 느껴집니다. \n체력을 20회복한다.";
                yield return new WaitUntil(() => NextMoveCheck);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                All.Manager().player.LifeChange(20);

                break;
            case 1:
                text.text = "자리를 뜨려는 순간, 허브에서 뿜어져나온 어두운 기운이 당신을 감쌉니다. 순간 당황했지만 이것이 별다른 해를 끼치지 않으며, 심지어 몸을 숨겨줄 수 있다는 것을 깨달았습니다.\n은신 카드 한장 획득";
                yield return new WaitUntil(() => NextMoveCheck);
                BG.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                yield return StartCoroutine(All.Manager().card.CardAdd(Random.Range(0, 7), hide));
                break;
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
