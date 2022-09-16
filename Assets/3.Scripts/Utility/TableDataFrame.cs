using System.Collections;
using System.Collections.Generic;

#region Abilities
public class AbilityInfo
{
    public int idx;
    public string abtype;
    public float level_unit;
    public string name;
}
#endregion

#region SkillTabe
public class HealerSkillInfo
{
    public int idx;
    public string name;
    public string desc;
    public string skillType;
    public string prefabName;
    public bool ActiveType;
    public int AbilityIndex;
    public float CoolTime;
    public float active_time;
    public float skill_ability_0;
    public float skill_ability_1;
    public int Grade;
    public int NeedPoint;
    public int maxLevel;
    public int sideeffect_1;
    public int sideeffect_2;
    public int sideeffect_3;
    public int sideeffectvalue_1;
    public int sideeffectvalue_2;
    public int sideeffectvalue_3;
}
#endregion
