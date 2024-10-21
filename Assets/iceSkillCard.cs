using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceSkillCard : SkillCard
{
    public PointFunc PointFunc;
    public override void DefaultSet()
    {
        PointFunc.Register();
        base.DefaultSet();
    }
}
