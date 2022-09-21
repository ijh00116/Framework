using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public class Data_Character
    {
        Dictionary<AbilityContentType, AbilityValueCache> _cachedAbilityValue;
        Dictionary<AbilitiesType, float> _cachedAbilityNumericValue;

        AbilityValueCache _cachedskillAbilityValue;

        public Data_Character()
        {
            foreach(var skilldata in TableManager.Instance.healerskillList.skills)
            {
                var abilinfo = TableManager.Instance.AbilityList.abilities.Find(o => o.idx == skilldata.AbilityIndex);

                AbilitiesType abiltype = EnumExtention.ParseToEnum<AbilitiesType>(abilinfo.abtype);
                _cachedskillAbilityValue.Add(skilldata.AbilityIndex, abiltype);
            }

            _cachedAbilityValue.Add(AbilityContentType.Skill,_cachedskillAbilityValue);

            for(int i=0; i<(int)AbilitiesType.End; i++)
            {
                AbilitiesType _Type = (AbilitiesType)i;
                if(_cachedAbilityNumericValue.ContainsKey(_Type))
                {
                    var _value = CalculateAbilityValue(_Type);
                    _cachedAbilityNumericValue[_Type] = _value;
                }
                else
                {
                    float data = CalculateAbilityValue(_Type);
                    _cachedAbilityNumericValue.Add(_Type, data);
                }
            }
        }

        public void SetAbilityValue(AbilityContentType contentType,AbilitiesType abilvalue, int abilIndex, float value)
        {
            if(_cachedAbilityValue.ContainsKey(contentType))
            {
                if (_cachedAbilityValue[contentType].HasValue(abilvalue))
                {
                    _cachedAbilityValue[contentType].SetValue(abilvalue, abilIndex, value);
                }
            }
            if(_cachedAbilityNumericValue.ContainsKey(abilvalue))
            {
                _cachedAbilityNumericValue[abilvalue] = CalculateAbilityValue(abilvalue);
            }
        }

        public float GetAbilityValue(AbilitiesType _type)
        {
            if(_cachedAbilityNumericValue.ContainsKey(_type))
            {
                return _cachedAbilityNumericValue[_type];
            }
            else
            {
                return 0;
            }
        }
        public float CalculateAbilityValue(AbilitiesType _type)
        {
            float totalvlue = 0;

            foreach(var data in _cachedAbilityValue)
            {
                if(data.Value.HasValue(_type))
                {
                    totalvlue+=data.Value.GetValue(_type);
                }
            }

            return totalvlue;
        }
    }
}