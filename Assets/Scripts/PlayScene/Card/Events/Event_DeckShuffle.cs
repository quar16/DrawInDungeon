using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_DeckShuffle : Events
{
    int option;
    int allCardCount = 0;
    protected override IEnumerator EventProcessing()
    {
        yield return new WaitUntil(() => isSelected);
        text.text = "점점어지러워지던 흐름이 가라앉기 시작하고 이윽고 당신의 눈앞에는 새로운 길이 열렸습니다. 이제부터 어떻게 될지는 당신에게 달려있습니다.";
        yield return new WaitForSeconds(1);
        BG.SetActive(false);
        yield return new WaitForSeconds(0.5f);

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

        yield return new WaitUntil(() => allCardCount == 0);

        for (int i = 0; i < 7; i++)
            foreach (Card card in All.Manager().card.lines[i])
                Destroy(card.gameObject);

        for (int i = 0; i < 7; i++)
            All.Manager().card.lines[i].Clear();
        //카드 리스트 비우고 카드 삭제

        for (int i = 0; i < 7; i++)
            yield return StartCoroutine(All.Manager().card.LineMaking(i));
        All.Manager().flow.turn++;
        Destroy(gameObject);
    }

    public void Select(int _option)
    {
        foreach (GameObject @object in buttons)
            @object.SetActive(false);
        isSelected = true;
        option = _option;
        checkButton.SetActive(true);
    }

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
