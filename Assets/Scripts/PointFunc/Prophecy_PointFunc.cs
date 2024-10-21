using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prophecy_PointFunc : PointFunc
{
    public TextMeshPro text;
    public int turn;
    public override void Register()
    {
        turn = Random.Range(3, 7);
        text.text = turn.ToString();
        Vector3 basePoint = All.Manager().player.player.transform.position;
        text.transform.position = basePoint + new Vector3(0.5f, 1.5f, 0);
        text.gameObject.SetActive(true);
        base.Register();
    }
    bool first = true;
    public override IEnumerator CouFunc()
    {
        turn--;
        if (first)
        {
            first = false;
            yield return new WaitForSeconds(1);
            text.gameObject.SetActive(false);
        }
        if (turn == 0)
        {
            All.Manager().skill.magnification *= 2f;
        }
        if (turn == -1)
        {
            text.gameObject.SetActive(true);
            text.fontSize = 3;
            text.text = "예언의 끝...";

            All.Manager().skill.magnification /= 2f;

            yield return new WaitForSeconds(1);
            text.gameObject.SetActive(false);

            StartCoroutine(Unregist());
        }
        yield break;
    }
    IEnumerator Unregist()
    {
        yield return new WaitUntil(() => All.Manager().point.PointProcessEnd);
        Unregister();
        Destroy(gameObject);
    }
}
