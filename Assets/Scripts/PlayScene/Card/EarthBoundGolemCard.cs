using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class EarthBoundGolemCard : Card
{
    public EarthboundGolem BossPrefab;
    public int allCardCount;

    protected override IEnumerator MouseUp()
    {
        if (isFront && mouseOn)
        {
            cardUse = true;
            All.Manager().sound.ChangeBGM(1);
            All.Manager().card.TouchPrevent.SetActive(true);
            All.Manager().card.bossFighting = 2;
            All.Manager().dungeon.isBossAlive[2] = false;
            tempO.SetActive(false);


            foreach (GameObject @object in All.Manager().Event.eventsIcon)
                Destroy(@object);

            All.Manager().Event.eventsIcon.Clear();

            foreach (EventStruct eventStruct in All.Manager().Event.eventsQueue)
            {
                if (eventStruct.monsters != null)
                    foreach (Monster monster in eventStruct.monsters)
                    {
                        Destroy(monster.gameObject);
                    }
            }
            All.Manager().Event.eventsQueue.Clear();


            foreach (Monster monster in All.Manager().monster.nowMonsters)
            {
                if (monster != null)
                    StartCoroutine(monster.Death());
            }
            EarthboundGolem boss = Instantiate(BossPrefab, new Vector3(0, 100, 0), Quaternion.identity);
            boss.ebgc = this;
            All.Manager().card.earthboundGolem = boss;
            yield return StartCoroutine(boss.Encount());
            All.Manager().monster.nowMonsters[1] = boss;
            //보스 소환& 보스의 동작과 동시에 카드 떨어지기
            for (int i = 0; i < 7; i++)
            {
                foreach (Card card in All.Manager().card.lines[i])
                {
                    StartCoroutine(CardFall(card.gameObject, i - 3));
                    allCardCount++;
                }
            }
            yield return new WaitUntil(() => allCardCount == 0);

            for (int i = 0; i < 7; i++)
                foreach (Card card in All.Manager().card.lines[i])
                    if (card != this)
                        Destroy(card.gameObject);

            for (int i = 0; i < 7; i++)
                All.Manager().card.lines[i].Clear();
            //카드 리스트 비우고 카드 삭제

            for (int i = 0; i < 7; i++)
                yield return StartCoroutine(All.Manager().card.LineMaking(i));
            //카드 매니저에게 보스전 전용 카드덱을 세팅하게 함
            //턴 재생
        }
        cardUse = false;
        All.Manager().card.TouchPrevent.SetActive(false);

        yield return null;
    }


    public IEnumerator CardFall(GameObject card, int x)
    {
        float rX = Random.Range(5, 15.0f) * (Random.Range(0, 2) * 2 - 1);
        float rY = Random.Range(5, 15.0f) * (Random.Range(0, 2) * 2 - 1);
        float scale = Random.Range(1, 1.01f);
        float y = Random.Range(7, 11f);
        Vector3 Dir = new Vector3(x * 2 + Random.Range(-2.5f, 2.5f), y, 0);
        while (card.transform.position.y > -8)
        {
            Dir += Vector3.down * 0.4f;
            card.transform.Rotate(new Vector3(rX, rY, 0));
            card.transform.localScale *= scale;
            card.transform.position += Dir / 60.0f;
            yield return null;
        }
        allCardCount--;
    }
}
