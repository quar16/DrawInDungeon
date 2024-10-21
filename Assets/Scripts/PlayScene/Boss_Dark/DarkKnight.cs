using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DarkKnight : Monster
{
    public int candleN
    {
        get
        {
            int m = 0;
            foreach (candleFunc candle in Candles)
                if (candle.isLight)
                    m++;
            return m;
        }
    }
    public TextMeshPro Next;
    public TextMeshPro DarkForceText;
    public int darkForceN;
    public candleFunc CandlePrefab;
    [HideInInspector]
    public candleFunc[] Candles;
    public Card lighting;
    public Card SunLightFirestone;
    public Card DarkForce;
    public Card FeelThePulse;
    public Card Torchlight;
    public Monster shadowBat;
    public SpriteRenderer DarkShroudPrefab;
    [HideInInspector]
    public SpriteRenderer DarkShroud;
    public override IEnumerator Encount()
    {
        firstGiven = false;
        
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();

        for (int i = 0; i < 6; i++)
        {
            candleFunc tempC = Instantiate(CandlePrefab);
            tempC.transform.position = new Vector3(-2.2f + i*2 + Random.Range(0, 0.5f), Random.Range(3.5f, 4.1f), 0);
            Candles[i] = tempC;
        }
        transform.position = new Vector3(13, 2.23f, -1);
        ChangeAnimation("Walk");
        for(int i = 0; i < 180; i++)
        {
            transform.position = new Vector3(13 - 8.5f * i / 179.0f, 2.23f, -1);
            yield return null;
        }
        ChangeAnimation("Attack");
        yield return new WaitForSeconds(0.7f);
        NextMove();
        ChangeAnimation("Idle");
    }

    public override IEnumerator turnProcess()
    {
        if (isDead)
            yield return StartCoroutine(Death());

        foreach (candleFunc candle in Candles)
        {
            if (candle.isLight)
            {
                yield return StartCoroutine(candle.turnProcess());
            }
        }
        turn--;
        if (turn == 0)
        {
            yield return StartCoroutine(Active(activeNumber));
            NextMove();
        }
        yield return StartCoroutine(TurnTextChanging());
    }

    public IEnumerator ActivatingDarkShroud()
    {
        DarkShroud = Instantiate(DarkShroudPrefab);
        for(int i = 0; i <= 20; i++)
        {
            DarkShroud.color = new Color(0, 0, 0, i * 9 / 255.0f);
            yield return null;
        }
        float rad = 0;
        float candleT = 0;
        while (DarkShroud != null)
        {
            candleT = (candleT * 10 + candleN) / 11f;
            DarkShroud.color = new Color(0, 0, 0, (180 - 25 * candleT + Mathf.Sin(rad) * 10) / 255.0f);
            rad += Random.Range(0.01f, 0.05f);
            if (rad > Mathf.PI * 2)
                rad -= Mathf.PI * 2;
            yield return null;
        }
    }

    public override IEnumerator Active(int active)//switch문으로 행동 조절
    {
        switch (active)
        {
            case -1:
            case 0:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                ChangeAnimation("Idle");
                All.Manager().player.LifeChange(-6);
                yield return new WaitForSeconds(0.2f);
                All.Manager().player.LifeChange(- darkForceN);
                break;
            case 1:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(0.7f);
                ChangeAnimation("Idle");
                All.Manager().player.LifeChange(-1);
                yield return new WaitForSeconds(0.2f);
                All.Manager().player.LifeChange(-darkForceN);
                yield return new WaitForSeconds(0.1f);
                All.Manager().player.LifeChange(-darkForceN);
                yield return new WaitForSeconds(0.1f);
                All.Manager().player.LifeChange(-darkForceN);
                break;
            case 2:
                ChangeAnimation("Attack");
                yield return new WaitForSeconds(1.1f);
                All.Manager().monster.SummonMonster(shadowBat);
                yield return new WaitForSeconds(1.1f);
                All.Manager().monster.SummonMonster(shadowBat);
                ChangeAnimation("Idle");
                break;
        }
        yield return null;
    }

    public bool feelthePulse = false;
    
    public override void NextMove()
    {
        if (feelthePulse)
        {
            feelthePulse = false;
            activeNumber = 1;
            turnSet(7);
            Next.text = "어둠의 파동";
            return;
        }
        if (Random.Range(0, 2) == 0)
        {
            if (All.Manager().monster.MonsterNumber == 1 && candleN < 2)
            {
                activeNumber = 2;
                turnSet(5);
                Next.text = "권속 소환";
                return;
            }
        }
        if (Random.Range(0, 5) != 0)
        {
            activeNumber = 0;
            Next.text = "내려치기";
            turnSet(3);
        }
        else
        {
            activeNumber = 1;
            Next.text = "어둠의 파동";
            turnSet(7);
        }
    }

    public bool firstGiven = false;
    public Card GetCard(int x, int y)//만들 카드를 정하는 부분
    {
        if (x == 3 && y == 7 && !firstGiven)
        {
            firstGiven = true;
            return Torchlight;
        }
        if (y == 0)
            return DarkForce;
        
        int d = Random.Range(0, 10);

        if (d <= 4)
            return All.Manager().card.nowItemSkill[Random.Range(0, All.Manager().card.nowItemSkill.Count)];
        else if (d <= 6)
            return lighting;
        else if (d <= 7)
            return SunLightFirestone;
        else if (d <= 8)
            return DarkForce;
        else
            return FeelThePulse;

    }

    public override void LifeChange(int damage)
    {
        if (candleN == 0)
        {
            StartCoroutine(All.Manager().monster.damageTextMoving(0, transform.position.x));
            return;
        }
        else if (candleN == 6)
            damage *= 4;
        else if (candleN >= 3)
            damage *= 2;

        StartCoroutine(All.Manager().monster.damageTextMoving(damage, transform.position.x));
        life = Mathf.Clamp(life + damage, 0, MaxLife);
        LifeText.text = life.ToString() + "/" + MaxLife.ToString();
        LifeGauge.localScale = new Vector2((float)life / MaxLife, LifeGauge.localScale.y);

        if (life == 0)
        {
            isDead = true;
            //여기서 바로 없에는게 아니라 턴이 지나는 순간에 죽이는걸로 여기서는 사망 확인만
        }
    }

    public IEnumerator CandleActivate()
    {
        for (int i = 0; i < 6; i++)
        {
            if (Candles[i].isLight)
                yield return StartCoroutine(Candles[i].LifeChange(1));
        }
        if (candleN != 6)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!Candles[i].isLight)
                {
                    Candles[i].candleImage.SetActive(true);
                    for (int j = 0; j <= 10; j++)
                    {
                        Candles[i].candleImage.transform.localScale = new Vector3(j / 100.0f, j / 100.0f, 1);
                        yield return null;
                    }
                    yield return StartCoroutine(Candles[i].LifeChange(3));
                    Candles[i].isLight = true;
                    break;
                }
            }
        }
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

        All.Manager().monster.nowMonsters[pos] = null;
        foreach (candleFunc candle in Candles)
            candle.isLight = true;
        yield return new WaitForSeconds(1f);
        Destroy(DarkShroud.gameObject);
        foreach (candleFunc candle in Candles)
            StartCoroutine(candle.LifeChange(-3));
        yield return new WaitForSeconds(1f);
        foreach (candleFunc candle in Candles)
            Destroy(candle.gameObject);
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
