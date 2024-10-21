using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public int stat_AD;
    public int stat_AP;
    public int stat_HEAL;
    public int stat_GUARD;

    public Skills nowSkill;
    public Monster nowMonster;
    public float magnification = 1;
    public int damageCalc(int damage, SkillType type)
    {
        if (type == SkillType.AD)
        {
            damage = AD(damage);
        }
        if (type == SkillType.AP)
        {
            damage = AP(damage);
        }
        return (int)(damage * magnification);
    }
    int AP(int damage)
    {
        return damage - stat_AP;
    }
    int AD(int damage)
    {
        return damage - stat_AD;
    }
}
