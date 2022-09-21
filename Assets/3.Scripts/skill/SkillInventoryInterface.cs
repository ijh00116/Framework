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
            //로드 인벤토리
            LoadskillData();
            //로드 인벤토리
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
        //플레이팹이지만 나중에 서버로 변경
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
        //플레이팹이지만 나중에 서버로 변경
    }
}