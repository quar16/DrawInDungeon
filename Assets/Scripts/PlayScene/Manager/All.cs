using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class All : MonoBehaviour
{
    [Header("Managers")]
    public CardManager card;
    public FlowManager flow;
    public EventManager Event;
    public PlayerManager player;
    public MonsterManager monster;
    public ItemManager item;
    public SkillManager skill;
    public DungeonManager dungeon;
    public SoundManager sound;
    public PointManager point;
    static All all;
    public static All Manager()
    {
        if(all == null)
            all = GameObject.FindGameObjectWithTag("AllManager").GetComponent<All>();
        return all;
    }
    public GameObject _camera;
    public static IEnumerator Camerashaking(float power, float time)
    {
        power *= 0.2f;
        float deltaTime = 0;
        GameObject camera = Manager()._camera;
        Vector3 origin = camera.transform.position;
        while (deltaTime < time)
        {
            deltaTime += Time.deltaTime;
            camera.transform.position = new Vector3(origin.x + Random.Range(-power, power), origin.y + Random.Range(-power, power), -15);
            power *= 0.9f;
            yield return new WaitForSeconds(0.03f);
        }
        camera.transform.position = origin;
    }

    [Header("tempParent")]
    public static Transform prefabParent;

    public static GameObject InstantiateP(GameObject prefab)
    {
        GameObject temp = Instantiate(prefab);
        temp.transform.SetParent(prefabParent);
        return temp;
    }

    public static void EffectSound(AudioClip clip)
    {
        AudioSource audio = Instantiate(GameObject.Find("AudioSource").GetComponent<AudioSource>());
        audio.clip = clip;
        audio.gameObject.SetActive(true);
        audio.Play();
    }
}
