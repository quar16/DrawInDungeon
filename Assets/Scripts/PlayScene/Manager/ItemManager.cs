using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Item[] startItem; //초기 아이템은 카드에 담겨 있지 않음
    [SerializeField]
    public Item[] nowItems = new Item[9];//9자리 단일 리스트로 변경
    float[] posX = { -8.033f, -6.81f, -5.585f };
    float[] posY = { 1.915f, 0.6f, -0.72f };
    public int influenceSum;
    public Transform ItemSlots;
    public GameObject itemUI;

    public float r = 0;
    private void Update()
    {
        r += 0.05f;
    }

    public void EquipItem(Item item)
    {
        for (int i = 0; i < 9; i++)
        {
            if (nowItems[i] == null)
            {
                Item temp = Instantiate(item);
                temp.transform.position = new Vector3(posX[i % 3], posY[i / 3], 0);
                nowItems[i] = temp;
                temp.gameObject.SetActive(true);

                temp.DefaultSet();

                GameObject _itemUI = Instantiate(itemUI, temp.transform.position, Quaternion.identity);
                if (item.itemName != null)
                {
                    _itemUI.transform.GetChild(1).GetComponent<TextMeshPro>().text = item.itemName;
                    _itemUI.transform.GetChild(2).GetComponent<TextMeshPro>().text = item.content;
                }
                temp.ItemUI = _itemUI;

                GameObject itemObj = Instantiate(temp.ItemObj, ItemSlots.GetChild(i).transform.position, temp.ItemObj.transform.rotation, ItemSlots.GetChild(i));
                itemObj.GetComponent<ItemObjFunc>().itemManager = this;
                itemObj.GetComponent<ItemObjFunc>().number = i;
                if (i < 4)
                    itemObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                else
                    itemObj.GetComponent<SpriteRenderer>().sortingOrder = -1;
                break;
            }
        }
        SkillReset();
    }

    public void StartItem()
    {
        //Item temp = Instantiate(startItem[UnityEngine.Random.Range(0, 1)]);//초기 아이템 개수 늘면 추가
        //temp.transform.position = new Vector3(-gapX, gapY, 0) + basePoint.position;
        //nowItems[0] = temp;
        EquipItem(startItem[0]);
        SkillReset();
    }

    public void SkillReset()
    {
        All.Manager().card.nowItemSkill.Clear();
        foreach (Item i in nowItems)
        {
            if (i != null)
            {
                foreach (SkillCard s in i.skillCards)
                {
                    All.Manager().card.nowItemSkill.Add(s);
                }
            }
        }
    }
}
