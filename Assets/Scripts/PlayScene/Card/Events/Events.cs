using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public GameObject BG;
    public GameObject[] buttons;
    public Text text;
    public int lineN;
    protected bool isSelected;
    protected bool NextMoveCheck = false;
    public GameObject checkButton;

    public void NextStep()
    {
        NextMoveCheck = true;
    }

    public IEnumerator EventPostProcessing()
    {
        yield return StartCoroutine(EventProcessing());
    }

    protected virtual IEnumerator EventProcessing()
    {
        yield return null;
    }
}
