using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackTree
{
    public class InGameController : MonoBehaviour, IController
    {

        [HideInInspector] public Data_Character PlayerCharacterdata;
        public IArchitecture GetArchitecture()
        {
            return InGameManager.Interface;
        }

        // Start is called before the first frame update
        void Start()
        {
            var gamemodel = this.GetModel<IGameModel>();
            StartCoroutine(TableManager.Instance.Load());
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                this.SendEvent<AddGold>();
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                this.SendEvent<AddScore>();
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                this.SendCommand<KillCommand>();
            }
        }
    }
}
