using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public Card []skills;
    public Card []tests;
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                All.Manager().item.EquipItem(All.Manager().card.itemPrefab[0].item);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                All.Manager().item.EquipItem(All.Manager().card.itemPrefab[1].item);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                All.Manager().item.EquipItem(All.Manager().card.itemPrefab[2].item);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                All.Manager().item.EquipItem(All.Manager().card.itemPrefab[3].item);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                All.Manager().item.EquipItem(All.Manager().card.itemPrefab[4].item);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                All.Manager().item.EquipItem(All.Manager().card.itemPrefab[5].item);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            All.Manager().dungeon.level++;
            All.Manager().dungeon.NowDungeonSet();

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            All.Manager().player.LifeChange(999);

        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            for (int i = 0; i < 3; i++)
                if (All.Manager().monster.nowMonsters[i] != null)
                    All.Manager().monster.nowMonsters[i].LifeChange(-999);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            for (int i = 0; i < 5; i++)
                StartCoroutine(All.Manager().card.CardAdd(i, skills[i]));
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            for (int i = 0; i < 7; i++)
                StartCoroutine(All.Manager().card.CardAdd(i, tests[i]));
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            foreach (Card card in All.Manager().card.lines[0])
                Destroy(card.gameObject);
            All.Manager().card.lines[0].Clear();
            StartCoroutine(All.Manager().card.LineMaking(0));
        }
    }
}
