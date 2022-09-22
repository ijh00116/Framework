using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

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
                GetTitleData();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveAppearance();
            }
        }
        #region Ŭ���� ��ũ��Ʈ
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
            Debug.Log(result.FunctionResult.ToString());
        }
        #endregion

        #region �α���
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
            //if(name==null)//�г��� ����
            //{
            //    SubmitNameButton();
            //}
            //else//�������� �����ֱ�
            //{
            //    GetLeaderboard();
            //}
            isLogin = true;
        }
        #endregion

        #region Ÿ��Ʋ������
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
    }
}
