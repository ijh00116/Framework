using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackTree
{
    public class SkillUIDisplay : MonoBehaviour
    {
        [SerializeField]Text skillname;
        [SerializeField] Text skilldesc;
        [SerializeField] Button levelupBtn;
        public HealerSkillInfo skillinfo;
        InventorySlot skillSlot;

        AbilityInfo abilInfo;
        public void Init(InventorySlot _skillSlot)
        {
            skillSlot = _skillSlot;
            skillSlot.onAfterUpdated += UpdateUI;

            abilInfo = TableManager.Instance.AbilityList.abilities.Find(o => o.idx == skillSlot.skillTabledata.AbilityIndex);

            skillname.text = skillSlot.skillTabledata.name;
            skilldesc.text = string.Format($"����: {skillSlot.skill.Level}, �ɷ�ġ: {abilInfo.abtype.ToString()}");

            levelupBtn.onClick.AddListener(LevelUp);
        }

        void UpdateUI()
        {
            skillname.text = skillSlot.skillTabledata.name;
            skilldesc.text = string.Format($"����: {skillSlot.skill.Level}, �ɷ�ġ: {abilInfo.abtype.ToString()}");
        }

        void LevelUp()
        {
            skillSlot.AddLevel(1);
        }
    }
}