using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum element { fire, water, air, earth, dark, light }

public class DungeonManager : MonoBehaviour
{
    public TextMesh levelText;
    public GameObject[] core;
    public GameObject effect;
    public int BossEncount = 1;
    public bool[] isBossAlive= new bool[7];
    public int level;
    public int influence;
    public bool[] elements;

    public void DungeonDefaultSet()
    {
        level = 4;
        influence = 10;
        BossEncount = -1;
        //elements[0] = true;
        //elements[1] = true;
        //elements[2] = true;
        //elements[3] = true;
        //elements[4] = true;
        //elements[5] = true;
        NowDungeonSet();
    }

    bool elementCheck;
    public void NowDungeonSet()
    {
        if (level >= 9)
            level = 8;

        Instantiate(effect, new Vector3(-6.8f, -3.45f, 0), Quaternion.identity);
        if (level / 3 > BossEncount)
        {
            BossEncount = level / 3;
        }
        StartCoroutine(visual());
        All.Manager().card.nowDungeonMonster.Clear();
        All.Manager().card.nowDungeonItem.Clear();
        All.Manager().card.nowDungeonEvent.Clear();

        foreach (MonsterCard card in All.Manager().card.monsterPrefab)
        {
            if (card.cardData.level <= level)
            {
                elementCheck = true;
                foreach (element element in card.cardData.requireElements)
                {
                    if (!elements[(int)element])
                        elementCheck = false;
                }
                if (elementCheck)
                    All.Manager().card.nowDungeonMonster.Add(card);
            }
        }
        foreach (ItemCard card in All.Manager().card.itemPrefab)
        {
            if (card.cardData.level <= level)
            {
                elementCheck = true;
                foreach (element element in card.cardData.requireElements)
                {
                    if (!elements[(int)element])
                        elementCheck = false;
                }
                if (elementCheck)
                    All.Manager().card.nowDungeonItem.Add(card);
            }
        }
        foreach (EventCard card in All.Manager().card.eventPrefab)
        {
            if (card.cardData.level <= level)
            {
                elementCheck = true;
                foreach (element element in card.cardData.requireElements)
                {
                    if (!elements[(int)element])
                        elementCheck = false;
                }
                if (elementCheck)
                    All.Manager().card.nowDungeonEvent.Add(card);
            }
        }
    }
    IEnumerator visual()
    {
        yield return new WaitForSeconds(1);
        levelText.text = level.ToString();
        for (int i = 0; i < 6; i++)
        {
            if (elements[i])
                core[i].SetActive(true);
        }
    }
}
