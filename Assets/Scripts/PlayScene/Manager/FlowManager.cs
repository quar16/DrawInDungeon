using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FlowManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(Flowing());
    }
    public int turn = 0;
    int turnN = 0;
    IEnumerator Flowing()
    {
        if (GameObject.FindGameObjectWithTag("SceneManager") != null)
        {
            SceneManage sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
            yield return new WaitWhile(() => sceneManage.SceneProcessIsGoing);
        }
        All.Manager().sound.ChangeBGM(0);
        All.Manager().card.DataSet();
        All.Manager().dungeon.DungeonDefaultSet();
        All.Manager().item.StartItem();
        All.Manager().player.LifeChange(0);
        yield return StartCoroutine(All.Manager().card.GameStart());
        while (true)
        {
            yield return new WaitWhile(() => turn == turnN);
            turnN = turn;
            yield return StartCoroutine(All.Manager().monster.MonsterTurnProcessing());
            if (All.Manager().monster.MonsterNumber == 0)
            {
                bool tempBool = false;
                while (All.Manager().Event.eventsQueue.Count != 0 && tempBool == false)
                {
                    tempBool= All.Manager().Event.eventsQueue[0].isMonsterEvent;
                    yield return StartCoroutine(All.Manager().Event.MoveToNextEvent());
                }
            }
            yield return StartCoroutine(All.Manager().point.PointActivating(Points.FLOW));
            Card.cardUse = false;
            All.Manager().card.TouchPrevent.SetActive(false);
        }
    }

    void Update()
    {
        //Debug.Log(Input.mousePosition);
    }
}
