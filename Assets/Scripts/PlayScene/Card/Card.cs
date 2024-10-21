using System.Collections;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
public partial class Card : MonoBehaviour
{
    public static bool cardUse = false;
    public GameObject front;
    public GameObject back;
    [HideInInspector]
    public CardManager cardManager;//이거 나중에 없엘수 있을까
    [HideInInspector]
    public bool isFront = false;
    protected bool isActivated;
    [HideInInspector]
    public int line;

    public CardData cardData;

    public virtual void DefaultSet()
    {
        tempO = Instantiate(transform.GetChild(0).gameObject);
        tempO.transform.parent = gameObject.transform;
        tempO.transform.localScale *= 3;//여기는 그래픽이 나오면 다시 바꿈
        tempO.transform.localPosition = new Vector3(-2, 2.2f, -3);
        tempO.SetActive(false);
    }
    protected GameObject tempO;

    public void Flip()
    {
        isFront = !isFront;
        StartCoroutine(Flipping());
    }

    IEnumerator Flipping()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.rotation = Quaternion.Euler(0, 18 * i, 0);
            yield return null;
        }
        front.SetActive(!front.activeSelf);
        back.SetActive(!back.activeSelf);
        for (int i = 6 - 1; i >= 0; i--)
        {
            transform.rotation = Quaternion.Euler(0, 18 * i, 0);
            yield return null;
        }
    }
    protected bool mouseOn;
    void OnMouseOver()
    {
        if (tempO != null)
            if (isFront && !cardUse)
            {
                mouseOn = true;
                tempO.SetActive(true);
            }
            else
            {
                mouseOn = false;
                tempO.SetActive(false);
            }
    }
    void OnMouseExit()
    {
        if (tempO != null)
        {
            mouseOn = false;
            tempO.SetActive(false);
        }

    }
    private IEnumerator OnMouseUp()
    {
        if (isFront && !cardUse)
        {
            All.EffectSound(All.Manager().card.useSound);
            yield return StartCoroutine(MouseUp());
            if (cardData.type != CardType.Skill && cardData.type != CardType.Boss && cardUse)
                cardManager.CardUse(line);
        }
    }

    protected virtual IEnumerator MouseUp()
    {
        yield return null;
    }

}

