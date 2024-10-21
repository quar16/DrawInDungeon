using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : Skills
{
    public int pos = 0;
    EarthboundGolem earthboundGolem;
    public override IEnumerator SkillActivating(Monster tempM = null)
    {
        earthboundGolem = GameObject.Find("EarthboundGolem(Clone)").GetComponent<EarthboundGolem>();
        earthboundGolem.playerPos = pos;
        switch (pos)
        {
            case -1:
                All.Manager().player.player.transform.position = new Vector3(-1.7f, 2.4f, 0);
                earthboundGolem.platformObj.transform.position = new Vector3(-1.7f, 4.2f, 0);
                break;
            case 1:
                All.Manager().player.player.transform.position = new Vector3(-1.7f, 3.3f, 0);
                earthboundGolem.platformObj.transform.position = new Vector3(-1.7f, 1.5f, 0);
                break;
        }
        yield break;
    }
}
