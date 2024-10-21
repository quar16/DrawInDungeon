using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EventCard : Card
{
    public GameObject eventIcon;
    public Events Event;
    public int type = 0;

    protected override IEnumerator MouseUp()
    {
        if (isFront&&mouseOn)
        {
            cardUse = true;
            All.Manager().card.TouchPrevent.SetActive(true);
            //이펙트 리스트 스크립트에 정보를 보내서 판정 범위를 처리, 실행일 경우 효과 리스트에서 인덱스로 검색해서 카드 기능을 실행
            if (type == 0)
                All.Manager().Event.AddEvent(eventIcon, Event);
            else
            {
                Events events = Instantiate(Event, GameObject.Find("Canvas").transform);
                events.lineN = line;
                events.gameObject.SetActive(true);
                yield return StartCoroutine(events.EventPostProcessing());
                Destroy(events.gameObject);
            }
        }
        yield return null;
    }
}

