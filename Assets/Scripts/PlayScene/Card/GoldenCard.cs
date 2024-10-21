using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldenCard : Card
{
    public TextMeshPro []EffectTexts;
    public GameObject Effect;
    protected override IEnumerator MouseUp()
    {
        if (isFront && mouseOn)
        {
            cardUse = true;
            All.Manager().card.TouchPrevent.SetActive(true);
            All.Manager().dungeon.level++;
            int r = Random.Range(0, 6);
            //if (All.Manager().dungeon.level % 3 == 0)
            for (int i = 0; i < 6; i++)
            {
                if (!All.Manager().dungeon.elements[(i + r) % 6])
                {
                    All.Manager().dungeon.elements[(i + r) % 6] = true;
                    break;
                }
            }
            All.Manager().dungeon.NowDungeonSet();
        }
        yield return null;
    }
}
