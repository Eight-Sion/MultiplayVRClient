using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class ModelsSyncManager : MonoBehaviour
{
    WebSocket ws;
    AvatarParameter _playerParam = new AvatarParameter();
    AvatarParameter _guestParam = new AvatarParameter();
    [SerializeField]
    public GameObject PlayerHeadTarget;
    [SerializeField]
    public GameObject PlayerLHandTarget;
    [SerializeField]
    public GameObject PlayerRHandTarget;
    [SerializeField]
    public GameObject PlayerMainModel;

    private Transform GuestHeadTarget;
    private Transform GuestLHandTarget;
    private Transform GuestRHandTarget;
    //private Transform GuestMainModel;
    bool initialized = false;
    public void SetGuest(Transform head, Transform lhand, Transform rhand, Transform main)
    {
        GuestHeadTarget = head;
        GuestLHandTarget = lhand;
        GuestRHandTarget = rhand;
        //GuestMainModel = main;
        initialized = true;
    }
    //サーバへ、メッセージを送信する
    public void SendParam()
    {
        if (ws.ReadyState == WebSocketState.Open)
        {
            _playerParam.headTarget_PositionX = PlayerHeadTarget.transform.position.x;
            _playerParam.headTarget_PositionY = PlayerHeadTarget.transform.position.y;
            _playerParam.headTarget_PositionZ = PlayerHeadTarget.transform.position.z;
            _playerParam.lHandTarget_PositionX = PlayerLHandTarget.transform.position.x;
            _playerParam.lHandTarget_PositionY = PlayerLHandTarget.transform.position.y;
            _playerParam.lHandTarget_PositionZ = PlayerLHandTarget.transform.position.z;
            _playerParam.rHandTarget_PositionX = PlayerRHandTarget.transform.position.x;
            _playerParam.rHandTarget_PositionY = PlayerRHandTarget.transform.position.y;
            _playerParam.rHandTarget_PositionZ = PlayerRHandTarget.transform.position.z;

            _playerParam.headTarget_RotateX = PlayerHeadTarget.transform.rotation.x;
            _playerParam.headTarget_RotateY = PlayerHeadTarget.transform.rotation.y;
            _playerParam.headTarget_RotateZ = PlayerHeadTarget.transform.rotation.z;
            _playerParam.headTarget_RotateW = PlayerHeadTarget.transform.rotation.w;
            _playerParam.lHandTarget_RotateX = PlayerLHandTarget.transform.rotation.x;
            _playerParam.lHandTarget_RotateY = PlayerLHandTarget.transform.rotation.y;
            _playerParam.lHandTarget_RotateZ = PlayerLHandTarget.transform.rotation.z;
            _playerParam.lHandTarget_RotateW = PlayerLHandTarget.transform.rotation.w;
            _playerParam.rHandTarget_RotateX = PlayerRHandTarget.transform.rotation.x;
            _playerParam.rHandTarget_RotateY = PlayerRHandTarget.transform.rotation.y;
            _playerParam.rHandTarget_RotateZ = PlayerRHandTarget.transform.rotation.z;
            _playerParam.rHandTarget_RotateW = PlayerRHandTarget.transform.rotation.w;
            //_playerParam.mainModel_PositionX = GuestMainModel.transform.position.x;
            //_playerParam.mainModel_PositionY = GuestMainModel.transform.position.y;
            //_playerParam.mainModel_PositionZ = GuestMainModel.transform.position.z;
            ws.Send(_playerParam.ToString());
        }
    }

    //サーバから受け取ったメッセージを、ChatTextに表示する
    public void RecvParam(string param)
    {
        _guestParam.FromString(param);
    }
    //サーバの接続が切れたときのメッセージを、ChatTextに表示する
    public void RecvClose()
    {

    }
    public void ApplyParam()
    {

        GuestHeadTarget.position = new Vector3(
            _guestParam.headTarget_PositionX,
            _guestParam.headTarget_PositionY,
            _guestParam.headTarget_PositionZ);
        GuestLHandTarget.position = new Vector3(
            _guestParam.lHandTarget_PositionX,
            _guestParam.lHandTarget_PositionY,
            _guestParam.lHandTarget_PositionZ);
        GuestRHandTarget.position = new Vector3(
            _guestParam.rHandTarget_PositionX,
            _guestParam.rHandTarget_PositionY,
            _guestParam.rHandTarget_PositionZ);

        GuestHeadTarget.rotation = new Quaternion(
                _guestParam.headTarget_RotateX,
                _guestParam.headTarget_RotateY,
                _guestParam.headTarget_RotateZ,
                _guestParam.headTarget_RotateW);
        GuestLHandTarget.rotation = new Quaternion(
            _guestParam.lHandTarget_RotateX,
            _guestParam.lHandTarget_RotateY,
            _guestParam.lHandTarget_RotateZ,
            _guestParam.lHandTarget_RotateW);
        GuestRHandTarget.rotation = new Quaternion(
            _guestParam.rHandTarget_RotateX,
            _guestParam.rHandTarget_RotateY,
            _guestParam.rHandTarget_RotateZ,
            _guestParam.rHandTarget_RotateW);
        //GuestMainModel.localPosition = new Vector3(
        //    _guestParam.mainModel_PositionX,
        //    _guestParam.mainModel_PositionY,
        //    _guestParam.mainModel_PositionZ);
    }
    void Start()
    {
        //接続処理。接続先サーバと、ポート番号を指定する
        ws = new WebSocket("ws://192.168.10.2:8888/");
        ws.Connect();

        //送信ボタンが押されたときに実行する処理「SendText」を登録する
        //sendButton.onClick.AddListener(SendText);
        //サーバからメッセージを受信したときに実行する処理「RecvText」を登録する
        ws.OnMessage += (sender, e) => RecvParam(e.Data);
        //サーバとの接続が切れたときに実行する処理「RecvClose」を登録する
        ws.OnClose += (sender, e) => RecvClose();
    }
    private void LateUpdate()
    {
        SendParam();
        if (initialized)
        {
            ApplyParam();
        }
    }
}