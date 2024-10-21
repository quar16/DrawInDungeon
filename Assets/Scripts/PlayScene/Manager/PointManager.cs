using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Points { HIT, ATTACK, FLOW }

public class PointManager : MonoBehaviour
{
    public List<PointFunc> attackPointFuncs;
    public List<PointFunc> hitPointFuncs;
    public List<PointFunc> flowPointFuncs;

    public List<PointFunc> PointFuncs(Points pos)
    {
        switch (pos)
        {
            case Points.ATTACK:
                return attackPointFuncs;
            case Points.HIT:
                return hitPointFuncs;
            case Points.FLOW:
                return flowPointFuncs;
        }
        return null;
    }
    public bool PointProcessEnd = false;
    public IEnumerator PointActivating(Points points)
    {
        PointProcessEnd = false;
        foreach (PointFunc pointFunc in PointFuncs(points))
        {
            yield return StartCoroutine(pointFunc.CouFunc());
        }
        PointProcessEnd = true;
        
    }
}
