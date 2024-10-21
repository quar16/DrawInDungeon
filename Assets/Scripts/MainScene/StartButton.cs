using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void Click()
    {
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>().Loading(1, 2);
    }
}
