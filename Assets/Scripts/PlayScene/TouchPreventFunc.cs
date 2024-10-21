using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TouchPreventFunc : MonoBehaviour
{
    public SpriteRenderer sr;
    private void OnEnable()
    {
        sr.color = Color.clear;
        StartCoroutine(Change());
    }
    IEnumerator Change()
    {
        for(int i = 0; i < 10; i++)
        {
            sr.color = new Color(0, 0, 0, i / 20.0f);
            yield return null;
        }
    }
}
