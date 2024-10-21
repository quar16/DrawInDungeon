using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CardType { Skill, Item, Monster, Event, Gold, Boss }

[System.Serializable]
public struct CardData
{
    public CardType type;
    public int level;
    public element[] requireElements;
}

public class CardManager : MonoBehaviour
{
    //public CardData cardData;//xml파일에서 불러올 데이터 대신
    public Transform cards;
    public GameObject Crystal;
    public GameObject TouchPrevent;
    public Card goldenCard;
    public GameObject UnknownBackImange;//?뒷면
    public AudioClip flipSound;
    public AudioClip useSound;

    public int bossFighting = 0;
    public Card[] BossCard;
    public DarkKnight darkKnight;
    public EarthboundGolem earthboundGolem;
    //골렘

    public MonsterCard[] monsterPrefab;
    public SkillCard[] skillPrefab;
    public ItemCard[] itemPrefab;
    public EventCard[] eventPrefab;

    public List<MonsterCard> nowDungeonMonster;
    public List<SkillCard> nowItemSkill;
    public List<ItemCard> nowDungeonItem;
    public List<EventCard> nowDungeonEvent;

    public List<Card>[] lines = new List<Card>[7];//이거 건드는건 나중에

    public bool Dispose = false;

    public void DataSet()
    {
        monsterPrefab = Resources.LoadAll<MonsterCard>("Monsters");
        skillPrefab = Resources.LoadAll<SkillCard>("Skills");
        itemPrefab = Resources.LoadAll<ItemCard>("Items");
        eventPrefab = Resources.LoadAll<EventCard>("Events");
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 7; i++)
        {
            All.EffectSound(flipSound);
            lines[i] = new List<Card>();
            for (int j = 0; j < 8; j++)
            {
                CardCreate(i, j);

                yield return new WaitForSeconds(0.01f * j);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 7; i++)
        {
            lines[i][7].Flip();
        }
    }


    public IEnumerator LineMaking(int i)//첫 세팅 이후에 라인을 만들 때
    {
        All.EffectSound(flipSound);
        for (int j = 0; j < 8; j++)
        {
            CardCreate(i, j);

            yield return new WaitForSeconds(0.01f * j);
        }
        yield return new WaitForSeconds(0.1f);
        lines[i][7].Flip();
    }
    private void CardCreate(int i, int j)
    {
        Card tempCD;
        switch (bossFighting)
        {
            default:
                tempCD = Instantiate(GetCard(j), Crystal.transform.position, Quaternion.identity);
                break;
            case 0:
                tempCD = Instantiate(GetCard(j), Crystal.transform.position, Quaternion.identity);
                break;
            case 1:
            tempCD = Instantiate(darkKnight.GetCard(i, j), Crystal.transform.position, Quaternion.identity);//만들 카드를 정하는 부분
                break;
            case 2:
            tempCD = Instantiate(earthboundGolem.GetCard(i, j), Crystal.transform.position, Quaternion.identity);//만들 카드를 정하는 부분
                break;
        }

        tempCD.transform.parent = cards;
        tempCD.cardManager = this;
        tempCD.DefaultSet();
        tempCD.line = i;
        lines[i].Add(tempCD);
        StartCoroutine(cardMove(tempCD.gameObject, LinePos(i, j)));
    }
    int itemCount = 0;
    public Card GetCard(int j)//만들 카드를 정하는 부분
    {
        itemCount--;
        if (j == 0)
        {
            if (All.Manager().dungeon.isBossAlive[All.Manager().dungeon.level / 3])
                return BossCard[All.Manager().dungeon.level / 3];
            else
                return goldenCard;
        }
        int i = All.Manager().item.influenceSum;
        int d = All.Manager().dungeon.influence;
        if (i > Random.Range(0, i + d))//플레이어와 던전의 영향력 계산
        {
            return nowItemSkill[Random.Range(0, nowItemSkill.Count)];
        }
        else
        {
            d = Random.Range(0, 10);
            if (d == 0 && itemCount <= 0)
            {
                itemCount = 56;
                return nowDungeonItem[Random.Range(0, nowDungeonItem.Count)];
            }
            else if (d < 4)
            {
                return nowDungeonEvent[Random.Range(0, nowDungeonEvent.Count)];
            }
            else
            {
                return nowDungeonMonster[Random.Range(0, nowDungeonMonster.Count)];
            }
        }
    }

    IEnumerator cardMove(GameObject temp, Vector3 ArrivalPT)
    {
        for(int i = 0; i < 30; i++)
        {
            temp.transform.position = Vector3.Lerp(temp.transform.position, ArrivalPT, i / 29.0f);
            yield return null;
        }
        //아직은 수정사항이 없는 부분
    }

    public IEnumerator CardAdd(int x, Card card)
    {
        Card tempCard = Instantiate(card, LinePos(x, lines[x].Count), Quaternion.Euler(0, 90, 0));

        tempCard.front.SetActive(true);
        tempCard.back.SetActive(false);
        tempCard.isFront = true;
        tempCard.transform.parent = cards;
        tempCard.cardManager = this;
        tempCard.line = x;
        lines[x][lines[x].Count - 1].Flip();
        lines[x].Add(tempCard);
        for(int i = 0; i < 12; i++)
        {
            tempCard.transform.rotation = Quaternion.Euler(0, 90 - 7.5f * i, 0);
            yield return null;
        }
        tempCard.DefaultSet();
        yield return null;
    }

    public void CardUse(int i)
    {
        Destroy(lines[i][lines[i].Count - 1].gameObject);
        lines[i].RemoveAt(lines[i].Count - 1);
        if (lines[i].Count != 0)
        {
            lines[i][lines[i].Count - 1].Flip();
        }
        else
        {
            StartCoroutine(LineMaking(i));
        }
        All.Manager().flow.turn++;
    }

    Vector3 basePoint = new Vector3(-1, -1.3f, 0);

    Vector3 LinePos(int i,int j)
    {
        Vector3 temp;
        temp = basePoint + new Vector3(1.5f * i, -0.4f * j, j * -0.1f);
        return temp;
    }

    public GameObject target;

    public GameObject allArea;
    public GameObject []monsterArea;
    public GameObject plaformArea;

    public void EffectOn(int index)
    {
        target.transform.position = Input.mousePosition;
        target.SetActive(true);

        switch (index)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    Monster monster = All.Manager().monster.nowMonsters[i];
                    if (monster != null)
                    {
                        monsterArea[i].GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(monster.EffectTarget.transform.position);
                        monsterArea[i].SetActive(true);
                    }
                }
                break;
            case 1:
                allArea.SetActive(true);
                break;
            case 2:
                plaformArea.SetActive(true);
                break;

        }
    }
    public void EffectMove()
    {
        target.transform.position = Input.mousePosition;
    }
    public void EffectOff(int index)
    {
        target.SetActive(false);

        switch (index)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                    monsterArea[i].SetActive(false);
                break;
            case 1:
                allArea.SetActive(false);
                break;
            case 2:
                plaformArea.SetActive(false);
                break;

        }
    }
}
