using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SkillCard[] skillCards;
    public GameObject ItemObj;//나중에 스크립트
    public GameObject ItemUI;
    public PointFunc[] pointFuncs;

    public string itemName;
    [TextArea(3, 5)]
    public string content;

    public void DefaultSet()
    {
        foreach(PointFunc point in pointFuncs)
        {
            point.Register();
        }
    }

    void OnMouseOver()
    {
        if (ItemUI != null)
            if (!Card.cardUse)
                ItemUI.SetActive(true);
            else
                ItemUI.SetActive(false);
    }
    void OnMouseExit()
    {
        if (ItemUI != null)
            ItemUI.SetActive(false);

    }
}
