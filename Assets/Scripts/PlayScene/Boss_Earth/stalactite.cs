using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactite : Skills
{
    EarthboundGolem earthboundGolem;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        earthboundGolem = GameObject.Find("EarthboundGolem(Clone)").GetComponent<EarthboundGolem>();
        if (earthboundGolem.playerPos != -1)
        {
            StartCoroutine(All.Camerashaking(1, 2f));
            StartCoroutine(earthboundGolem.stalactiteCardActivating(3, GetComponent<Card>().line));

            foreach (stoneFunc stone in earthboundGolem.stone)
                stone.blocked = false;
        }
        else
        {
            foreach (stoneFunc stone in earthboundGolem.stone)
                stone.blocked = true;
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(earthboundGolem.stone[Random.Range(0, 3)], new Vector3(Random.Range(-2.5f, -0.5f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.3f);
    }
}
