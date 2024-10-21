using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightStalkerClickFunc : MonoBehaviour
{
    public NightStalkerPointFunc nspf;
    public Card hide;
    private void OnMouseDown()
    {
        if (!Card.cardUse && nspf.turn == 0)
        {
            All.Manager().skill.stat_AP -= 4;
            nspf.turn = 10;
            nspf.effect.SetActive(false);
            StartCoroutine(All.Manager().card.CardAdd(Random.Range(0, 7), hide));
        }
    }
}
