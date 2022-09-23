using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

namespace BlackTree
{
    public class PlayfabManager : Monosingleton<PlayfabManager>
    {
        string myplayfabId;
        public bool isLogin = false;

        public SkillInventoryObject skillinventory;
        protected override void Init()
        {
            base.Init();
            Login();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                Execute();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetInventory();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                PurchaseItem();
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                SetInventoryCustomData();
            }
        }
        #region 클라우드 스크립트
        public void Execute()
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "hello",
                FunctionParameter = new { name = "dds" }
            };
            PlayFabClientAPI.ExecuteCloudScript(request, OnExecuteSuccess, OnError);
        }

        private void OnExecuteSuccess(ExecuteCloudScriptResult result)
        {
           // Debug.Log(result.ToString());
        }
        #endregion

        #region 로그인
        void Login()
        {
            var request = new LoginWithCustomIDRequest { 
                CustomId="imjaehyun",
                CreateAccount=true,
                InfoRequestParameters=new GetPlayerCombinedInfoRequestParams { 
                    GetPlayerProfile=true,
                }
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }

        private void OnSuccess(LoginResult result)
        {
            myplayfabId = result.PlayFabId;
            Debug.Log($"Successful login/account create!::{myplayfabId}");

            //GetVirtualCurrencies();
            //GetTitleData();
            //string name = null;
            //if(result.InfoResultPayload.PlayerProfile.DisplayName!=null)
            //    name = result.InfoResultPayload.PlayerProfile.DisplayName;
            //if(name==null)//닉네임 설정
            //{
            //    SubmitNameButton();
            //}
            //else//리더보드 보여주기
            //{
            //    GetLeaderboard();
            //}
            isLogin = true;
        }
        #endregion

        #region 타이틀데이터
        void GetTitleData()
        {
            PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataRecieved, OnError);
        }

        void OnTitleDataRecieved(GetTitleDataResult result)
        {
            if (result.Data == null)
            {
                Debug.Log("No message!");
                return;
            }
            Debug.Log(result.Data["Message"]);

            int.Parse(result.Data["Multiplier"]);
        }

        public void SaveAppearance()
        {
            Dictionary<string, string> characterdatadic = new Dictionary<string, string>();

            var _json = Newtonsoft.Json.JsonConvert.SerializeObject(skillinventory.Container);
            characterdatadic.Add("skill", _json.ToString());

            var request = new UpdateUserDataRequest
            {
                Data = characterdatadic
            };

            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        }

        private void OnDataSend(UpdateUserDataResult obj)
        {
            var _data = obj.Request.ToJson();

            Debug.Log("success user data sended");
        }
        #endregion

        public void OnError(PlayFabError obj)
        {
            Debug.Log(obj.ErrorMessage);
            Debug.Log(obj.GenerateErrorReport());
        }

        public void SetInventoryCustomData()
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "SetCustomItemdata",
                FunctionParameter = new { itemid = "5994484AEBAA5D0", equip = "true"}
            };
            PlayFabClientAPI.ExecuteCloudScript(request, OnExecuteSuccess, OnError);
        }

        public void PurchaseItem()
        {
            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
            {
                // In your game, this should just be a constant matching your primary catalog
                CatalogVersion = "CharacterItem",
                ItemId = "910001",
                Price = 0,
                VirtualCurrency = "GD"
            }, PurchaseItem_Result, OnError);
        }

        public void PurchaseItem_Result(PurchaseItemResult result)
        {
            Debug.Log(result.Items.ToString());
        }

        public void GetInventory()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
        }

        void OnGetUserInventorySuccess(GetUserInventoryResult result)
        {
            foreach(var _item in result.Inventory)
            {
                Debug.Log(_item.CustomData.ToString());
            }
            //Debug.Log(result.Inventory.ToString());
        }
    }
}
