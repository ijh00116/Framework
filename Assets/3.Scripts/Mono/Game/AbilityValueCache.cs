using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public enum AbilitiesType
    {
        AttackUp,
        AttackDown,
        HpUp,
        ArmorUp,
        End
    }
    public enum AbilityContentType
    {
        Skill,
        End
    }

    public class AbilityValueCache
    {
        public class Value
        {
            public float value;
            public int AbilityIdx;
            public Value(float _value)
            {
                value= _value;
            }
        }

        public Dictionary<AbilitiesType, Dictionary<int,Value>> values { get; private set; }

        public AbilityValueCache()
        {
            values = new Dictionary<AbilitiesType, Dictionary<int, Value>>();
        }

        public void Add(int abilIdx,AbilitiesType abiltype)
        {
            if(!values.ContainsKey(abiltype))
            {
                Value _value = new Value(0);
                var newvalue = new Dictionary<int, Value>();
                newvalue.Add(abilIdx, _value);
                values.Add(abiltype, newvalue);
            }
            else
            {
                var valuelist=values[abiltype];
                if(valuelist.ContainsKey(abilIdx)==false)
                {
                    valuelist.Add(abilIdx, new Value(0));
                }
            }
        }

        public bool HasValue(AbilitiesType _type)
        {
            return values.ContainsKey(_type);
        }

        public float GetValue(AbilitiesType abilType,int abilIdx)
        {
            if (values.ContainsKey(abilType))
            {
                values[abilType].TryGetValue(abilIdx, out Value value);
                return value.value;
            }
            Debug.LogError("ability type not exist");
            return 0;       
        }

        public float GetValue(AbilitiesType abilityType)
        {
            float data = 0;
            if (values.ContainsKey(abilityType))
            {
                Dictionary<int, Value> valueList = values[abilityType];
                foreach (KeyValuePair<int, Value> _data in valueList)
                {
                    data += _data.Value.value;
                }
            }
            return data;
        }

        public void SetValue(AbilitiesType abilityType, int abilIndex, float value)
        {
            if (values.ContainsKey(abilityType))
            {
                Dictionary<int, Value> valueList = values[abilityType];
                foreach (KeyValuePair<int, Value> data in valueList)
                {
                    if (data.Key == abilIndex)
                        data.Value.value = value;
                }
            }
        }
    }
}
