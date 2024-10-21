using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//씬을 다루는 스크립트, 이전에 사용하던대로, 딱히 손볼 것 없음
public class SceneManage : MonoBehaviour
{
    public Image BGg;
    public Text LImage;
    public Image sg;
    public GameObject Camera;
    string[] sceneName = { "", "MainScene", "PlayScene" };

    public void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        StartCoroutine(SG());
    }
    
    //GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>().Loading(0, 1);
    //씬 로딩할 때 쓰는 호출

    IEnumerator SG()
    {
        sg.gameObject.SetActive(true);
        for (int i = 0; i <= 180; i++)
        {
            sg.color = new Color(1, 1, 1, i / 180f);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        for (int i = 180; i >= 0; i--)
        {
            sg.color = new Color(1, 1, 1, i / 180f);
            yield return null;
        }
        yield return new WaitForSeconds(1);

        sg.gameObject.SetActive(false);
        Camera.SetActive(false);
        Loading(0, 1);
    }

    public void Loading(int CloseNumber, int OpenNumber)//씬을 변경하는 함수
    {
        StartCoroutine(LoadScene(CloseNumber, OpenNumber));
    }

    public bool SceneProcessIsGoing = false;

    IEnumerator LoadScene(int CloseNumber, int OpenNumber)
    {
        SceneProcessIsGoing = true;
        if(CloseNumber != 0)
        {
            float l = 0;
            BGg.color = new Color(0, 0, 0, 1);
            LImage.color = new Color(1, 1, 1, 0);
            BGg.gameObject.SetActive(true);
            while (l < 1)
            {
                l += Time.deltaTime;
                BGg.color = new Color(0, 0, 0, l);
                LImage.color = new Color(1, 1, 1, l);
                yield return null;
            }
            yield return StartCoroutine(SceneProcess(false, CloseNumber));
        }
        
        if (OpenNumber != 0)
        {
            yield return StartCoroutine(SceneProcess(true, OpenNumber));

            float l = 1;
            BGg.color = new Color(0, 0, 0, 0);
            LImage.color = new Color(1, 1, 1, 0);
            while (l > 0)
            {
                l -= Time.deltaTime;
                BGg.color = new Color(0, 0, 0, l);
                LImage.color = new Color(1, 1, 1, l);
                yield return null;
            }
            BGg.gameObject.SetActive(false);
        }
        SceneProcessIsGoing = false;
    }

    IEnumerator SceneProcess(bool open, int sceneNumber)
    {
        AsyncOperation asyncLoad;

        if (open)
        {
            asyncLoad = SceneManager.LoadSceneAsync(sceneName[sceneNumber], LoadSceneMode.Additive);
        }
        else
        {
            asyncLoad = SceneManager.UnloadSceneAsync(sceneName[sceneNumber]);
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

    
}