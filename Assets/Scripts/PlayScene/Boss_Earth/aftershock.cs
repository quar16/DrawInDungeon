using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aftershock : Skills
{
    EarthboundGolem earthboundGolem;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        earthboundGolem = GameObject.Find("EarthboundGolem(Clone)").GetComponent<EarthboundGolem>();
        if (earthboundGolem.playerPos != 1)
        {
           yield return StartCoroutine(earthboundGolem.Crack(GetComponent<Card>().line));
        }
        yield return new WaitForSeconds(0.3f);
    }
}
