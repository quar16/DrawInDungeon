using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Monster[] monsterPrefab;
    public List<Monster> nowDungeonMonster;
    public Monster[] nowMonsters = new Monster[3];
    public Vector3 basePoint;
    public TextMeshPro damageText;

    public IEnumerator MonsterTurnProcessing()
    {
        foreach(Monster monster in nowMonsters)
        {
            if (monster != null)
            {
                yield return StartCoroutine(monster.turnProcess());
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public int MonsterNumber
    {
        get
        {
            int m = 0;
            for (int i = 0; i < 3; i++)
            {
                if (nowMonsters[i] != null)
                {
                    m++;
                }
            }
            return m;
        }
    }

    public IEnumerator damageTextMoving(int damage, float x)
    {
        TextMeshPro text = Instantiate(damageText, new Vector3(x, 3, 0), Quaternion.identity);
        text.text = damage.ToString();
        if (damage < 0)
            text.color = Color.red;
        else if (damage > 0)
            text.color = Color.green;
        else
            text.color = Color.white;

        float moveY = Random.Range(0.7f, 1.3f);
        float moveX = Random.Range(-1.3f, -0.7f);
        Vector3 original = text.transform.position;
        for (int i = 0; i < 20; i++)
        {
            text.transform.position = original + new Vector3(moveX * i / 20f, moveY * Mathf.Sin(i / 35.0f * Mathf.PI), 0);
            text.color += new Color(0, 0, 0, Mathf.Cos(i / 40.0f * Mathf.PI));
            yield return null;
        }
        Destroy(text.gameObject);
    }
    public GameObject summonEffect;
    public void SummonMonster(Monster monster)
    {
        for (int i = 0; i < 3; i++)
        {
            if (nowMonsters[i] == null)
            {
                Monster temp = Instantiate(monster);
                nowMonsters[i] = temp;
                temp.pos = i;
                temp.transform.position = basePoint + new Vector3(2.5f * i, 0.5f - i * 0.5f, 0);
                temp.transform.GetChild(0).localScale *= 0.8f;
                temp.gameObject.SetActive(true);
                StartCoroutine(temp.Encount());
                break;
            }
        }
    }
    public IEnumerator CardSummonMonster(Monster monster)
    {
        for (int i = 0; i < 3; i++)
        {
            if (nowMonsters[i] == null)
            {
                Monster temp;

                if (monster.gameObject.scene.name == null)
                    temp = Instantiate(monster);
                else
                    temp = monster;

                nowMonsters[i] = temp;
                temp.pos = i;
                temp.transform.position = basePoint + new Vector3(2.5f * i, 5.5f - i * 0.5f, 0);
                temp.gameObject.SetActive(true);
                for (int y = 0; y < 5; y++)
                {
                    temp.transform.position += Vector3.down;
                    yield return null;
                }
                GameObject TempEffect = Instantiate(summonEffect, temp.transform.position, Quaternion.identity);
                StartCoroutine(DelayedDestroy(TempEffect));
                StartCoroutine(temp.Encount());
                break;
            }
        }
    }

    IEnumerator DelayedDestroy(GameObject target, float delayTime = 1)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(target);
    }

    public Monster target()
    {
        for(int i = 0; i < 3; i++)
        {
            if (nowMonsters[i] != null && nowMonsters[i].isSelected)
                return nowMonsters[i];
        }
        return null;
    }

}
