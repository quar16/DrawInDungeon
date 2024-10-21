using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EventStruct
{
    public bool isMonsterEvent;
    public Events events;
    public List<Monster> monsters;//몬스터가 추가되면서 수정될 부분
}

public class EventManager : MonoBehaviour
{
    public List<GameObject> eventsIcon = new List<GameObject>();
    public List<EventStruct> eventsQueue = new List<EventStruct>();//이거 두개 통합 못하나
    public Vector3 basePoint;
    public Canvas canvas;

    public void AddMonster(Monster monster, GameObject icon)
    {
        if (eventsQueue.Count == 0 && All.Manager().monster.MonsterNumber != 3)
        {
            StartCoroutine(All.Manager().monster.CardSummonMonster(monster));
        }
        else if (eventsQueue.Count != 0 && eventsQueue[eventsQueue.Count - 1].isMonsterEvent == true && eventsQueue[eventsQueue.Count - 1].monsters.Count != 3)
        {
            eventsQueue[eventsQueue.Count - 1].monsters.Add(Instantiate(monster));
            Destroy(eventsIcon[eventsIcon.Count - 1]);
            eventsIcon.RemoveAt(eventsIcon.Count - 1);
            GameObject temp = Instantiate(icon);//하나의 빈 오브젝트에 추가 하는 방식 적용필요
            temp.transform.position = basePoint + Vector3.right * 0.38f * eventsQueue.Count;
            eventsIcon.Add(temp);
            temp.SetActive(true);
        }
        else
        {
            EventStruct tempEvs = new EventStruct();
            tempEvs.isMonsterEvent = true;
            tempEvs.monsters = new List<Monster>();
            tempEvs.monsters.Add(Instantiate(monster));
            eventsQueue.Add(tempEvs);

            GameObject temp = Instantiate(icon);//하나의 빈 오브젝트에 추가 하는 방식 적용필요
            temp.transform.position = basePoint + Vector3.right * 0.38f * eventsQueue.Count;
            eventsIcon.Add(temp);
            temp.SetActive(true);
        }
    }

    public void AddEvent(GameObject eventIcon, Events eventPrefab)
    {
        GameObject temp = Instantiate(eventIcon);
        EventStruct tempEvs = new EventStruct();
        Events events = Instantiate(eventPrefab, canvas.transform);
        tempEvs.events = events;
        eventsQueue.Add(tempEvs);
        temp.transform.position = basePoint + Vector3.right * 0.38f * eventsQueue.Count;
        temp.SetActive(true);
        eventsIcon.Add(temp);
    }

    public IEnumerator MoveToNextEvent()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(eventsIcon[0]);
        eventsIcon.RemoveAt(0);
        if (eventsQueue[0].isMonsterEvent)
        {
            foreach(Monster monster in eventsQueue[0].monsters)
            {
                StartCoroutine(All.Manager().monster.CardSummonMonster(monster));
            }
        }
        else
        {
            eventsQueue[0].events.gameObject.SetActive(true);
            yield return StartCoroutine(eventsQueue[0].events.EventPostProcessing());
            Destroy(eventsQueue[0].events.gameObject);
        }
        foreach (GameObject @object in eventsIcon)
        {
            @object.transform.position -= Vector3.right * 0.38f;
        }
        eventsQueue.RemoveAt(0);
        yield return null;
    }
}
