using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFunc : MonoBehaviour
{
    public Points points;

    public virtual void Register()
    {
        All.Manager().point.PointFuncs(points).Add(this);
    }
    public virtual void Unregister()
    {
        All.Manager().point.PointFuncs(points).Remove(this);
    }

    public virtual IEnumerator CouFunc()
    {
        yield return null;
    }
}
