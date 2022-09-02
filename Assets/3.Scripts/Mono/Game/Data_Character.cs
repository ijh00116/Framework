using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public class Data_Character
    {
        Dictionary<AbilityValueType, AbilityValueCache> _cachedAbilityValue;
        Dictionary<AbilitiesType, float> _cachedAbilityNumericValue;
    }
}
