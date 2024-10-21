using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EarthboundGolem : Monster
{

    public TextMeshPro Next;
    public PointFunc platformSensor;
    public EarthBoundGolemCard ebgc;
    public stoneFunc[] stone;
    public GameObject platformObj;
    public SpriteRenderer dustBG;

    public GameObject earthquakeEffect;
    
    public List<rockFunc> rocks;
    
    public Card rockCard;
    public Card platform;
    public Card stalactite;
    public Card aftershock;

    public int playerPos;

    public GameObject ExposionEffect;

    string defaultPos = "Idle";

    public override IEnumerator Encount()
    {
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();
        platformSensor.Register();
        transform.position = new Vector3(6.5f, 8, -1);

        yield return StartCoroutine(All.Camerashaking(0.6f, 0.4f));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(All.Camerashaking(1.2f, 0.4f));
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 11; i++)
        {
            transform.position = new Vector3(7, 12 - 9.44f * i / 10f, -1);
            yield return null;
        }
        transform.position = new Vector3(6.5f, 2.56f, -1);
        StartCoroutine(All.Camerashaking(1.8f, 0.7f));

        NextMove();
        ChangeAnimation(defaultPos);
    }

    public override IEnumerator turnProcess()
    {
        if (isDead)
        {
            yield return StartCoroutine(Death());
            yield break;
        }

        turn--;
        yield return StartCoroutine(E_TurnTextChanging());
        if (turn == 0)
        {
            dustOn = false;
            yield return StartCoroutine(Active(activeNumber));
            NextMove();
        }
        if (TimesandActive)
            StartCoroutine(TimeFlowing());
    }
    public TextMeshPro TimeCountText;
    IEnumerator TimeFlowing()
    {
        TimeCountText.gameObject.SetActive(true);
        yield return new WaitWhile(() => Card.cardUse);
        for (int i = 5; i >= 0; i--)
        {
            TimeCountText.text = i.ToString();
            TimeCountText.transform.rotation = Quaternion.Euler(90, 0, 0);
            float second = 0;
            while (second <= 1)
            {
                if (Card.cardUse)
                {
                    TimeCountText.transform.rotation = Quaternion.Euler(90, 0, 0);
                    TimeCountText.gameObject.SetActive(false);
                    yield break;
                }
                
                if (second > 0.3f && i == 0)
                    break;

                if (second < 0.3f)
                {
                    TimeCountText.transform.rotation = Quaternion.Euler(90 - second * 300f, 0, 0);
                }

                if (second > 0.7f)
                {
                    TimeCountText.transform.rotation = Quaternion.Euler((second - 0.7f) * 300, 0, 0);
                }

                yield return null;
                second += Time.deltaTime;
            }
        }

        Card.cardUse = true;
        All.Manager().card.TouchPrevent.SetActive(true);
        All.Manager().card.EffectOff(0);
        All.Manager().card.EffectOff(1);
        All.Manager().card.EffectOff(2);
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 60; i++)
        {
            TimeCountText.transform.rotation = Quaternion.Euler(i * 1.5f, 0, 0);
            yield return null;
        }
        TimeCountText.transform.rotation = Quaternion.Euler(90, 0, 0);
        TimeCountText.transform.localScale = Vector3.one;

        All.Manager().flow.turn++;
    }

    public IEnumerator Crack(int index)
    {
        StartCoroutine(All.Camerashaking(0.5f, 1f));

        int top = -1;
        int max = -1;
        for (int i = 0; i < 7; i++)
        {
            if (All.Manager().card.lines[i].Count >= max && i != index)
            {
                if (All.Manager().card.lines[i].Count > max)
                    top = i;
                else if (Random.Range(0, 2) == 0)
                    top = i;
                max = All.Manager().card.lines[i].Count;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(Shaking(All.Manager().card.lines[top][max - 1 - i].gameObject));
            allCardCount++;
        }
        yield return new WaitUntil(() => allCardCount == 0);


        for (int i = 0; i < 3; i++)
        {
            Destroy(All.Manager().card.lines[top][max - 1 - i].gameObject);
        }


        for (int i = 0; i < 3; i++)
        {
            All.Manager().card.lines[top].RemoveAt(max - 1 - i);
        }

        All.Manager().card.lines[top][All.Manager().card.lines[top].Count - 1].Flip();
    }

    public IEnumerator stalactiteCardActivating(int count, int forbidden = -1)
    {
        bool check = false;
        for (int i = 1; i < 8; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (All.Manager().card.lines[j].Count == i && count != 0)
                {
                    if (forbidden != j)
                    {
                        check = true;
                        StartCoroutine(All.Manager().card.CardAdd(j, rockCard));
                        rocks.Add(All.Manager().card.lines[j][All.Manager().card.lines[j].Count - 1].GetComponent<rockFunc>());
                        count--;
                    }
                }
            }
            if (check)
                yield return new WaitForSeconds(0.2f);
            check = false;
        }
    }

    public IEnumerator Shaking(GameObject card)
    {
        float rX = Random.Range(1, 4.0f) * (Random.Range(0, 2) * 2 - 1);
        float rY = Random.Range(1, 4.0f) * (Random.Range(0, 2) * 2 - 1);
        float scale = Random.Range(1, 1.005f);
        float y = Random.Range(1, 3f);
        Vector3 Dir = new Vector3(Random.Range(-2.5f, 2.5f), y, 0);
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

    IEnumerator rockDestroying(rockFunc rock)
    {
        rocks.Remove(rock);
        Card card = rock.GetComponent<Card>();
        All.Manager().card.lines[card.line].Remove(card);
        for (int i = 0; i < 6; i++)
        {
            rock.transform.rotation = Quaternion.Euler(0, 18 * i, 0);
            rock.transform.localScale = new Vector3(1, 1 + i * 0.16f, 1);
            yield return null;
        }
        Destroy(rock.gameObject);
    }

    IEnumerator EarthQuaking()
    {
        List<GameObject> tempL = new List<GameObject>();
        for (int i = 0; i < 9; i++)
        {
            GameObject tempO = Instantiate(earthquakeEffect, new Vector3(5 - 1.1f * i, 1, 0), Quaternion.identity);
            tempL.Add(tempO);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject gameObject in tempL)
            Destroy(gameObject);
    }

    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        switch (active)
        {
            case 0://낙석
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                ChangeAnimation(defaultPos);
                StartCoroutine(All.Camerashaking(1, 2f));
                if (playerPos != -1)
                {
                    StartCoroutine(stalactiteCardActivating(10));
                    foreach (stoneFunc stone in stone)
                        stone.blocked = false;
                }
                else
                {
                    foreach (stoneFunc stone in stone)
                        stone.blocked = true;
                    anger++;
                }
                yield return new WaitForSeconds(0.1f);
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(stone[Random.Range(0, 3)], new Vector3(Random.Range(-2.5f, -0.5f), 7, 0), Quaternion.identity);
                    yield return new WaitForSeconds(0.1f);
                    if (playerPos != -1)
                        All.Manager().player.LifeChange(-1);
                }

                break;
            case 1://지진
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                StartCoroutine(All.Camerashaking(1, 2f));
                ChangeAnimation(defaultPos); 
                if (playerPos != 1)
                {
                    All.Manager().player.LifeChange(-15);
                    StartCoroutine(EarthQuaking());
                }
                else
                {
                    anger++;
                    break;
                }

                int top = -1;
                int max = -1;
                for(int i = 0; i < 7; i++)
                {
                    if (All.Manager().card.lines[i].Count >= max)
                    {
                        if (All.Manager().card.lines[i].Count > max)
                            top = i;
                        else if (Random.Range(0, 2) == 0)
                            top = i;
                        max = All.Manager().card.lines[i].Count;
                    }
                }

                foreach (Card card in All.Manager().card.lines[top])
                {
                    StartCoroutine(Shaking(card.gameObject));
                    allCardCount++;
                }
                yield return new WaitUntil(() => allCardCount == 0);


                foreach (Card card in All.Manager().card.lines[top])
                    Destroy(card.gameObject);
                All.Manager().card.lines[top].Clear();
                yield return StartCoroutine(All.Manager().card.LineMaking(top));

                break;
            case 2://암석폭발
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                ChangeAnimation(defaultPos);
                for (int i = rocks.Count-1; i != -1; i--){
                    if (rocks[i] == null)
                    {
                        rocks.Remove(rocks[i]);
                    }
                }
                foreach(rockFunc rock in rocks)
                {
                    if(rock == null)
                    {
                        rocks.Remove(rock);
                    }
                }
                int count = rocks.Count;
                List<GameObject> tempList = new List<GameObject>();
                for(int i = count; i != 0; i--)
                {
                    int t = Random.Range(0, rocks.Count);
                    StartCoroutine(rockDestroying(rocks[t]));
                    tempList.Add(Instantiate(ExposionEffect, new Vector3(Random.Range(-2.5f, -0.5f), 1.8f, 0), Quaternion.identity));
                    All.Manager().player.LifeChange(-3);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 7; i++)
                {
                    if (!All.Manager().card.lines[i][All.Manager().card.lines[i].Count - 1].isFront)
                    {
                        All.Manager().card.lines[i][All.Manager().card.lines[i].Count - 1].Flip();
                    }
                }
                foreach (GameObject gameObject in tempList)
                    Destroy(gameObject);
                break;
            case 3://모래먼지
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.8f);
                ChangeAnimation(defaultPos);
                //모래먼지 소환하는 어떤 함수
                dustOn = true;
                break;
            case 4://시간의 모래시계
                TimesandActive = true;
                defaultPos = "Walk";
                ChangeAnimation(defaultPos);
                StartCoroutine(Sandglass());
                dustBG.gameObject.SetActive(true);
                dustBG.gameObject.transform.position = new Vector3(2.8f, 2.7f, 0);
                for(int i = 0; i < 60; i++)
                {
                    dustBG.color += new Color(0, 0, 0, 1 / 500f);
                    yield return null;
                }
                yield return new WaitForSeconds(1.1f);
                break;
        }
        yield return null;
    }
    public DustFunc dust;
    public IEnumerator Sandglass()
    {
        while (!isDead)
        {
            for (int i = 0; i < 3; i++)
                Instantiate(dust, new Vector3(10, Random.Range(0, 5.4f), 0), Quaternion.identity);
            yield return null;
        }

        for (int i = 0; i < 60; i++)
        {
            dustBG.color -= new Color(0, 0, 0, 1 / 500f);
            yield return null;
        }
    }

    bool Timesand = false;
    bool TimesandActive = false;
    bool dustOn = false;
    public int anger = 0;

    public override void NextMove()
    {
        if (life * 2 < MaxLife && !Timesand)
        {
            Timesand = true;
            activeNumber = 4;
            turn = 1;
            StartCoroutine(E_TurnTextChanging());
            Next.text = "시간의 모래시계";
        }
        else if (anger >= 2)
        {
            anger -= 2;
            activeNumber = 3;
            turn = 1;
            StartCoroutine(E_TurnTextChanging());
            Next.text = "모래먼지";

        }
        else if (rocks.Count >= 7 && Random.Range(0, 4) != 0)
        {
            activeNumber = 2;
            turn = 7;
            StartCoroutine(E_TurnTextChanging());
            Next.text = "암석 폭발";
        }
        else if (Random.Range(0, 2) == 0)
        {
            activeNumber = 0;
            turn = 5;
            StartCoroutine(E_TurnTextChanging());
            Next.text = "낙석";
        }
        else
        {
            activeNumber = 1;
            turn = 4;
            StartCoroutine(E_TurnTextChanging());
            Next.text = "지진";
        }

        if (dustOn)
        {
            Next.text = "?????";
        }
    }

    protected IEnumerator E_TurnTextChanging()
    {
        if (dustOn)
            turnText.text = "?";
        else
            turnText.text = turn.ToString();

        turnText.color = Color.clear;
        Vector2 tempV = turnText.GetComponent<RectTransform>().localScale;
        turnText.GetComponent<RectTransform>().localScale *= 2;
        float max = 10;
        for (int i = 0; i <= max; i++)
        {
            yield return null;
            turnText.color = new Color(1, 1, 1, i / max);
            turnText.GetComponent<RectTransform>().localScale = tempV * (2 - i / max);
        }
    }

    public Card GetCard(int x, int y)//만들 카드를 정하는 부분
    {
        int d = Random.Range(1, 17);

        if (d <= 8)
            return All.Manager().card.nowItemSkill[Random.Range(0, All.Manager().card.nowItemSkill.Count)];
        else if (d <= 9)
            return platform;
        else if (d <= 14)
            return stalactite;
        else
            return aftershock;

    }

    public override IEnumerator Death()
    {
        for (int i = 0; i < 7; i++)
        {
            int j = 0;
            foreach (Card card in All.Manager().card.lines[i])
            {
                StartCoroutine(CardFall(card.gameObject, i - 3, j));
                j++;
                allCardCount++;
            }
        }

        ChangeAnimation("Dead");
        yield return new WaitForSeconds(2);
        transform.position = Vector3.up * 100;

        yield return new WaitUntil(() => allCardCount == 0);

        for (int i = 0; i < 7; i++)
            foreach (Card card in All.Manager().card.lines[i])
                Destroy(card.gameObject);

        for (int i = 0; i < 7; i++)
            All.Manager().card.lines[i].Clear();
        //카드 리스트 비우고 카드 삭제

        platformSensor.Unregister();
        All.Manager().card.bossFighting = 0;

        for (int i = 0; i < 7; i++)
            yield return StartCoroutine(All.Manager().card.LineMaking(i));

        All.Manager().sound.ChangeBGM(0);
        transform.position = Vector3.up * 100;
        StartCoroutine(Destroying());
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    int allCardCount = 0;
    IEnumerator CardFall(GameObject card, int x, int y)
    {
        yield return new WaitForSeconds(Random.Range(0, 40) / 10f);
        float r = Random.Range(6, 15.0f) * (Random.Range(0, 2) * 2 - 1);
        card.transform.GetChild(1).Rotate(new Vector3(0, 0, Random.Range(-33, 33.0f)));
        Vector3 Dir = new Vector3(0, Random.Range(-8, -5.0f), 0);
        while (card.transform.position.y > -7)
        {
            card.transform.Rotate(new Vector3(0, r, 0));
            card.transform.position += Dir / 60.0f;
            yield return null;
        }
        allCardCount--;
    }
}
