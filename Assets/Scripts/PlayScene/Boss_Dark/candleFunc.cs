using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candleFunc : MonoBehaviour
{
    public GameObject candleImage;
    public bool isLight;
    public bool changed;
    public int life;

    public IEnumerator turnProcess()
    {
        if (!changed)
            yield return StartCoroutine(LifeChange(-1));
        else
            changed = false;
    }
    public IEnumerator LifeChange(int value)
    {
        life = Mathf.Clamp(life + value, 0, 3);
        Vector3 lastSize = candleImage.transform.localScale;
        for (float i = 0; i <= 1; i += 0.1f)
        {
            candleImage.transform.localScale = Vector3.Lerp(lastSize, new Vector3(0.033f, 0.033f, 1) * life, i);
            yield return null;
        }
        if (life == 0)
        {
            isLight = false;
            candleImage.SetActive(false);
        }
        if (value > 0)
            changed = true;
    }
}
