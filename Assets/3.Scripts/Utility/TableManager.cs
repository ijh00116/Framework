using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public class Ability_DataTable
    {
        public List<AbilityInfo> abilities;
    }
    public class HealerSkill_DataTable
    {
        public List<HealerSkillInfo> skills;
    }

    public class TableManager : Monosingleton<TableManager>
    {
        public Ability_DataTable AbilityList;
        public HealerSkill_DataTable healerskillList;
        protected override void Init()
        {
            base.Init();
            AbilityList = new Ability_DataTable();
            healerskillList = new HealerSkill_DataTable();
        }

        public IEnumerator Load()
        {
            AbilityList = ReadData<Ability_DataTable>("DT_Abillities.xlsx");
            healerskillList = ReadData<HealerSkill_DataTable>("DT_KR_HealerSkill_Test.xlsx");
            yield break;
        }

        T ReadData<T>(string fileName)
        {
            string _path = "Tables/" + fileName;
            TextAsset jsonString = Resources.Load<TextAsset>(_path);

            if (jsonString != null)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString.text);
            }
            return default;
        }

        [ContextMenu("delete")]
        public void PlayerprefsDelete()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
