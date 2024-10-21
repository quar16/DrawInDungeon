using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public int lifeTime = 0;
    private void Start()
    {
        StartCoroutine(Destroying());
    }
    IEnumerator Destroying()
    {
        int life = 1;
        if (lifeTime != 0)
            life = lifeTime;
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
