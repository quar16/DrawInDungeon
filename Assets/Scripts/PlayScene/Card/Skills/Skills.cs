using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { AD, AP, GUARD, HEAL, NONE }

public class Skills : MonoBehaviour
{
    public GameObject[] effect;
    public AudioClip[] effectSound;
    public SkillType skillType;
    public virtual IEnumerator SkillActivating(Monster tempM = null)
    {
        yield return null;
    }

    public IEnumerator Effect(int index, Monster monster, float time, int sound)
    {
        GameObject tempEffect = Instantiate(effect[index], monster.EffectTarget.transform.position, Quaternion.identity);
        if (sound != -1)
        {
            All.EffectSound(effectSound[sound]);

        }
        yield return new WaitForSeconds(time);
        //Destroy(tempEffect);
    }
    public IEnumerator Effect(int index, Vector3 pos, float time, int sound)
    {
        GameObject tempEffect = Instantiate(effect[index], pos, Quaternion.identity);
        if (sound != -1)
        {
            All.EffectSound(effectSound[sound]);

        }
        yield return new WaitForSeconds(time);
        //Destroy(tempEffect);
    }
}
