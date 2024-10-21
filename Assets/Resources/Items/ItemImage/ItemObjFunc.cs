using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjFunc : MonoBehaviour
{
    public float number = 0;
    public ItemManager itemManager;
    private void Start()
    {
        StartCoroutine(Moving());
    }
    IEnumerator Moving()
    {
        Vector3 origin = transform.localPosition;
        while (true)
        {
            transform.localPosition = origin + Vector3.up * Mathf.Sin(itemManager.r + number / 4f * Mathf.PI) * 0.2f;
            yield return null;
        }

    }
}
