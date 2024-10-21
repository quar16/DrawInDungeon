using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Sprite[] playerImages;
    public GameObject[] playerPrefab;
    public SpriteRenderer player;
    public Transform LifeGauge;
    public TextMeshPro LifeText;
    public Transform DefendGauge;
    public TextMeshPro DefendText;
    public TextMeshPro damageText;
    private int MaxLife = 100;
    private int life = 100;
    public int defend = 0;
    public AudioClip clip;
    public void PlayerMoving(int index)//0:idle 1:attack 2:hit
    {
        //player.sprite = playerImages[index];
        Instantiate(playerPrefab[index - 1], player.transform.position, Quaternion.identity);
    }
    public float alleviation = 1;
    public void LifeChange(int value, int Change = 0)
    {
        int lastLife = life + defend;
        MaxLife = Mathf.Clamp(MaxLife + Change, 0, int.MaxValue);
        value = (int)(value * alleviation);
        if (value < 0)
        {
            StartCoroutine(All.Manager().point.PointActivating(Points.HIT));
            if (defend + value < 0)
            {
                value += defend;
                defend = 0;
                life = Mathf.Clamp(life + value, 0, MaxLife);
            }
            else
            {
                defend = Mathf.Clamp(defend + value, 0, int.MaxValue);
            }
        }
        else
        {
            life = Mathf.Clamp(life + value, 0, MaxLife);
        }

        StartCoroutine(damageTextMoving(life + defend - lastLife));
        if (value < 0)
        {
            All.EffectSound(clip);
            PlayerMoving(2);
        }

        DefendText.text = defend.ToString();
        DefendGauge.localScale = new Vector2(Mathf.Clamp((float)defend / MaxLife, 0f, 1f), DefendGauge.localScale.y);

        LifeText.text = life.ToString() + "/" + MaxLife.ToString();
        LifeGauge.localScale = new Vector2((float)life / MaxLife, LifeGauge.localScale.y);
        if (life == 0)
        {
            //Destroy(player);
            //Destroy(lifeText.gameObject);
        }
    }
    public void DefendChange(int value)
    {
        defend += value;
        DefendText.text = defend.ToString();
        DefendGauge.localScale = new Vector2(Mathf.Clamp((float)defend / MaxLife, 0f, 1f), DefendGauge.localScale.y);
    }

    IEnumerator damageTextMoving(int damage)
    {
        if (damage != 0)
        {
            TextMeshPro text = Instantiate(damageText, player.transform.position, Quaternion.identity);
            text.text = damage.ToString();
            if (damage < 0)
                text.color = Color.red;
            else
                text.color = Color.green;

            float moveY = Random.Range(0.7f, 1.3f);
            float moveX = Random.Range(-1.3f, -0.7f);
            Vector3 original = text.transform.position;
            for (int i = 0; i < 20; i++)
            {
                text.transform.position = original + new Vector3(moveX * i / 20f, moveY * Mathf.Sin(i / 35.0f * Mathf.PI), 0);
                text.color += new Color(0, 0, 0, Mathf.Cos(i / 40.0f * Mathf.PI));
                yield return null;
            }
            Destroy(text.gameObject);
        }
    }
}
