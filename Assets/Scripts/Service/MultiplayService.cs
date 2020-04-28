using Assets.Scripts.ServerShared.MessagePackObjects;
using Assets.Scripts.Service;
using Grpc.Core;
using MagicOnion.Client;
using Sample.Shared.Hubs;
using Sample.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplayService : IMultiplayHubReceiver
{
    private Channel channel;
    private IMultiplayService multiplayService;
    private IMultiplayHub multiplayHub;
    private Player _player = new Player();
    private Room _room = new Room();
    private string _target = "3.114.223.110:80";
    public bool IsConnected { get; private set; } = false;
    public VRMFactory VRMFactory;

    public MultiplayService(string target, Player player, VRMFactory vrmFactory, VoiceChatManager voiceChatManager)
    {
        _target = target;
        player.CopyTo(_player);
        VRMFactory = vrmFactory;
        VRMFactory.Create(_player.Name, _player.ModelName, AvatarType.Player);
        Open();
        string room = "testroom1";//UIができるまで固定
        JoinRoomAsync(_player, room);
        voiceChatManager.StartVoiceChat(_player.Name, room);
    }
    public void Update()
    {
        if (!IsConnected) return;
        var target = VRMFactory.Read(_player.Name);
        if (target.head == null || target.left == null || target.right == null || target.main == null) return;
        _player.HeadPosition = new Vector3(target.head.transform.position.x, target.head.transform.position.y, target.head.transform.position.z);
        _player.HeadRotation = new Quaternion(target.head.transform.rotation.x, target.head.transform.rotation.y, target.head.transform.rotation.z, target.head.transform.rotation.w);
        _player.LeftPosition = new Vector3(target.left.transform.position.x, target.left.transform.position.y, target.left.transform.position.z);
        _player.LeftRotation = new Quaternion(target.left.transform.rotation.x, target.left.transform.rotation.y, target.left.transform.rotation.z, target.left.transform.rotation.w);
        _player.RightPosition = new Vector3(target.right.transform.position.x, target.right.transform.position.y, target.right.transform.position.z);
        _player.RightRotation = new Quaternion(target.right.transform.rotation.x, target.right.transform.rotation.y, target.right.transform.rotation.z, target.right.transform.rotation.w);
        UpdatePlayerAsync(_player);
    }
    public async void Open()
    {
        if (IsConnected) return;
        try
        {
            this.channel = new Channel(_target, ChannelCredentials.Insecure);
            this.multiplayService = MagicOnionClient.Create<IMultiplayService>(channel);
            this.multiplayHub = StreamingHubClient.Connect<IMultiplayHub, IMultiplayHubReceiver>(this.channel, this);
            IsConnected = true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            IsConnected = false;
        }
    }

    public async void Close()
    {
        if (!IsConnected) return;
        IsConnected = false;
        await this.multiplayHub.DisposeAsync();
        await this.channel.ShutdownAsync();
    }
    public async void JoinRoomAsync(Player player, string roomName)
    {
        var result = await this.multiplayHub.JoinAsync(player, roomName);
        result.Players.ForEach(otherPlayer =>
        {
            if (otherPlayer.Name != _player.Name) VRMFactory.UpdateGuest(otherPlayer);
        });
    }
    public async void UpdatePlayerAsync(Player player)
        => await this.multiplayHub.UpdatePlayerAsync(player);
    public async void SendMessageAsync(string message)
        => await this.multiplayHub.SendMessageAsync(message);
    /// <summary>
    /// 普通のAPI通信のテスト用のメソッド
    /// </summary>
    async void SampleServiceTest(int x, int y)
    {
        var sumReuslt = await this.multiplayService.SumAsync(x, y);
        Debug.Log($"{nameof(sumReuslt)}: {sumReuslt}");

        var productResult = await this.multiplayService.ProductAsync(2, 3);
        Debug.Log($"{nameof(productResult)}: {productResult}");
    }

    /// <summary>
    /// リアルタイム通信のテスト用のメソッド
    /// </summary>
    async void SampleHubTest()
    {
        // 自分のプレイヤー情報を作ってみる
        var player = new Player
        {
            Name = "Minami",
        };

        // ゲームに接続する
        await this.multiplayHub.JoinAsync(player, "testRoom");

        // チャットで発言してみる
        await this.multiplayHub.SendMessageAsync("こんにちは！");

        // 位置情報を更新してみる
        player.HeadPosition = new Vector3(1, 0, 0);
        await this.multiplayHub.UpdatePlayerAsync(player);

        // ゲームから切断してみる
        await this.multiplayHub.LeaveAsync();
    }

    #region リアルタイム通信でサーバーから呼ばれるメソッド群

    public void OnJoin(Player player)
    {
        if(player.Name != _player.Name) VRMFactory.Create(player, AvatarType.Guest);
    }

    public void OnLeave(Player player)
    {
        if (player.Name != _player.Name) VRMFactory.Delete(player.Name);
    }

    public void OnSendMessage(string name, string message)
    {
        Debug.Log($"{name}: {message}");
    }

    public void OnUpdatePlayer(Player player)
    {
        //Debug.Log($"{player.Name}さんが移動しました: {{ x: {player.HeadPosition.x}, y: {player.HeadPosition.y}, z: {player.HeadPosition.z} }}");
    }
    public void OnUpdateRoom(Room room)
    {
        room.Players.ForEach(player => {
            if (player.Name != _player.Name) VRMFactory.UpdateGuest(player);
        });
    }

    #endregion
}