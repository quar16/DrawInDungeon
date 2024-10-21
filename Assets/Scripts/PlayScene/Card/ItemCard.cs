using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TMPro;
using UnityEngine;

public class ItemCard : Card
{
    public Item item;
    public TextMeshPro nameText;
    public TextMeshPro contentText;

    public override void DefaultSet()
    {
        if (item.itemName != "")
        {
            nameText.text = item.itemName;
            contentText.text = item.content;
        }
        base.DefaultSet();
    }

    protected override IEnumerator MouseUp()
    {
        if (isFront && mouseOn)
        {
            cardUse = true;
            All.Manager().card.TouchPrevent.SetActive(true);
            //이펙트 리스트 스크립트에 정보를 보내서 판정 범위를 처리, 실행일 경우 효과 리스트에서 인덱스로 검색해서 카드 기능을 실행
            All.Manager().item.EquipItem(item);
        }
        yield return null;
    }
}

