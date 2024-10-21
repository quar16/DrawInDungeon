using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFunc : Skills
{
    EarthboundGolem earthboundGolem;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        earthboundGolem = GameObject.Find("EarthboundGolem(Clone)").GetComponent<EarthboundGolem>();
        earthboundGolem.rocks.Remove(this);
        yield break;
    }
}
