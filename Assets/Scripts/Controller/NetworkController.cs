using Assets.Scripts.ServerShared.MessagePackObjects;
using Assets.Scripts.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public string PlayerName = "testPlayer1";
    public string ModelName = "Mitu";
    public string LoginServerAddress = "localhost:80";
    public string MultiPlayerServerAddress = "localhost:80";
    LoginService _loginService;
    MultiplayService _multiplayService = null;
    public VRMFactory VRMFactory;
    public VoiceChatManager VoiceChatManager;


    private void OnLogin()
    {
        if (_multiplayService == null)
        {
            _multiplayService = new MultiplayService(MultiPlayerServerAddress, new Player()
            {
                Name = PlayerName,
                ModelName = ModelName,
                HeadPosition = Vector3.zero,
                HeadRotation = Quaternion.identity,
                LeftPosition = Vector3.zero,
                LeftRotation = Quaternion.identity,
                RightPosition = Vector3.zero,
                RightRotation = Quaternion.identity,
            }, VRMFactory, VoiceChatManager);
        }
    }
    private void OnLogout()
    {
        if (_multiplayService != null)
        {
            _multiplayService.Close();
            _multiplayService = null;
        }
    }
    private void OnDestroy()
    {
        if (_multiplayService != null)
        {
            _multiplayService.Close();
            _multiplayService = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _loginService = new LoginService(OnLogin, OnLogout);
        OnLogin();//test
    }

    // Update is called once per frame
    void Update()
    {
        if(_multiplayService != null){
            _multiplayService.Update();
        }
    }
}
