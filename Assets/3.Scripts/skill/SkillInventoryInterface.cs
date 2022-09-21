using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackTree
{
    public class SkillInventoryInterface : MonoBehaviour
    {
        public SkillInventoryObject skillinventory;

        [SerializeField] Transform parent;
        [SerializeField] SkillUIDisplay uidisplayPrefab;
        public void Init()
        {
            //�ε� �κ��丮
            LoadskillData();
            //�ε� �κ��丮
            skillinventory.Init();

          

            foreach(var _slotdata in skillinventory.GetSlots)
            {
                var slotdisplay = Instantiate(uidisplayPrefab);
                slotdisplay.transform.SetParent(parent, false);
                slotdisplay.Init(_slotdata);
            }
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                SaveskillData();
            }
        }
        //�÷����������� ���߿� ������ ����
        void SaveskillData()
        {
            var savedata=Newtonsoft.Json.JsonConvert.SerializeObject(skillinventory.Container);
            PlayerPrefs.SetString("skill",savedata);
        }

        void LoadskillData()
        {
            var jsondata = PlayerPrefs.GetString("skill");
            skillinventory = new SkillInventoryObject();
            if (string.IsNullOrEmpty(jsondata)==false)
            {
                skillinventory.Container = Newtonsoft.Json.JsonConvert.DeserializeObject<Inventory>(jsondata);
            }
          
        }
        //�÷����������� ���߿� ������ ����
    }
}