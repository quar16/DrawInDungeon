using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePointFunc : PointFunc
{
    public iceSkillCard iceSkillCard;
    int life = 4;
    public override IEnumerator CouFunc()
    {
        if (iceSkillCard.isFront)
        {
            life--;
            if (life == 0)
            {
                yield return StartCoroutine(iceDestroying());
            }
        }
    }

    IEnumerator iceDestroying()
    {
        All.Manager().card.lines[iceSkillCard.line].Remove(iceSkillCard);
        for (int i = 0; i < 6; i++)
        {
            iceSkillCard.transform.rotation = Quaternion.Euler(0, 18 * i, 0);
            iceSkillCard.transform.localScale = new Vector3(1, 1 + i * 0.16f, 1);
            yield return null;
        }
        All.Manager().card.lines[iceSkillCard.line][All.Manager().card.lines[iceSkillCard.line].Count-1].Flip();

        Destroy(iceSkillCard.gameObject);
    }
}
