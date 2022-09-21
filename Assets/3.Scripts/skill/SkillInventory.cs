using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BlackTree
{
    public class SkillInventoryObject 
    {
        public Inventory Container = new Inventory();
        
        public List<InventorySlot> GetSlots=> Container.Slots;
        
        public void Init()
        {
            if(GetSlots.Count>0)
            {
                for (int i = 0; i < TableManager.Instance.healerskillList.skills.Count; i++)
                {
                    var skilldata = TableManager.Instance.healerskillList.skills[i];
                    var slot=GetSlots.Find(o => o.skill.idx == skilldata.idx);
                    slot.skillTabledata = skilldata;
                    slot.parent = this;
                }
            }
            else
            {
                for (int i = 0; i < TableManager.Instance.healerskillList.skills.Count; i++)
                {
                    var skilldata = TableManager.Instance.healerskillList.skills[i];
                    InventorySlot slot = new InventorySlot();
                    slot.skill = new Skill() { amount = 0, Equiped = false, idx = skilldata.idx, Level = 1 };
                    slot.skillTabledata = skilldata;
                    slot.parent = this;
                    GetSlots.Add(slot);
                }
            }
            
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> Slots = new List<InventorySlot>();
        public Inventory()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                Slots[i] = new InventorySlot();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                Slots[i].skill = new Skill();
            }
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        [System.NonSerialized] public SkillInventoryObject parent;
        [System.NonSerialized] public Action onAfterUpdated;

        public Skill skill;

        [System.NonSerialized]
        public HealerSkillInfo skillTabledata;

        public void AddAmount(int value)
        {
            skill.amount += value;
            UpdateSlot();
        }

        public void AddLevel(int value)
        {
            skill.Level += value;
            UpdateSlot();
        }

        public void EquipItem()
        {
            skill.Equiped = true;
            UpdateSlot();
        }

        public void UpdateSlot()
        {
            onAfterUpdated?.Invoke();
        }
    }
}
