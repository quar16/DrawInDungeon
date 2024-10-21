using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class MonsterCard : Card
{
    public GameObject icon;
    public Monster monster;

    protected override IEnumerator MouseUp()
    {
        if (isFront && mouseOn)
        {
            cardUse = true;
            All.Manager().card.TouchPrevent.SetActive(true);
            //이펙트 리스트 스크립트에 정보를 보내서 판정 범위를 처리, 실행일 경우 효과 리스트에서 인덱스로 검색해서 카드 기능을 실행
            All.Manager().Event.AddMonster(monster, icon);
        }
        yield return new WaitForSeconds(0.1f);
    }
}

