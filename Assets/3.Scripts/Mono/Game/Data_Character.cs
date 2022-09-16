using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public class Data_Character
    {
        Dictionary<AbilityValueType, AbilityValueCache> _cachedAbilityValue;
        Dictionary<AbilitiesType, float> _cachedAbilityNumericValue;

        AbilityValueCache _cachedskillAbilityValue;

        public Data_Character()
        {
            foreach(var skilldata in TableManager.Instance.healerskillList.skills)
            {
                var abilinfo = TableManager.Instance.AbilityList.abilities.Find(o => o.idx == skilldata.AbilityIndex);

                //AbilitiesType abiltype = EnumExtention.ParseToEnum<AbilitiesType>(abInfo.abtype);
                //_cachedRelicAbilityValue.Add(abidx, abiltype);
            }
        }
    }
}