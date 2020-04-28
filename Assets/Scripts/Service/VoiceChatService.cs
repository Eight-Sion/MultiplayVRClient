using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MonobitEngine;
using MonobitEngine.VoiceChat;
using System.Linq;

namespace Assets.Scripts.Service
{
    public class VoiceChatService : MonobitEngine.MonoBehaviour
    {
        /** ルーム内のプレイヤーに対するボイスチャット送信可否設定. */
        private Dictionary<MonobitPlayer, Int32> vcPlayerInfo = new Dictionary<MonobitPlayer, int>();

        /** 自身が所有するボイスアクターのMonobitViewコンポーネント. */
        private MonobitVoice myVoice = null;

        /** ボイスチャット送信可否設定の定数. */
        private enum EnableVC
        {
            ENABLE = 0,         /**< 有効. */
            DISABLE = 1,        /**< 無効. */
        }
        /// <summary>
        /// ボイスチャットサーバーへの接続処理
        /// </summary>
        /// <param name="playername"></param>
        /// <param name="roomname"></param>
        public void StartVoiceChat(string playername, string roomname)
        {
            MonobitNetwork.playerName = playername;
            MonobitNetwork.autoJoinLobby = true;
            MonobitNetwork.ConnectServer("SimpleVoiceChat_v1.0");
            Debug.Log("VC_StartConnecting!");
            StartCoroutine(WaitConnectedCoroutine(playername, roomname));
        }
        public void StopVoiceChat()
        {
            MonobitNetwork.LeaveRoom();
        }
        private void OnDestroy()
        {
            MonobitNetwork.LeaveRoom();
        }
        /// <summary>
        /// 接続待ち処理
        /// </summary>
        /// <param name="roomname"></param>
        /// <returns></returns>
        private IEnumerator WaitConnectedCoroutine(string playerName, string roomname)
        {
            yield return null;
            if (MonobitNetwork.isConnecting)
            {
                yield return new WaitForSecondsRealtime(3.0f);
                Debug.Log(string.Join(" / ", MonobitNetwork.GetRoomData().Select(x => x.name)));
                if (!MonobitNetwork.inRoom)
                {
                    Debug.Log("VC_Connected!");
                    if (MonobitNetwork.GetRoomData().ToList().FirstOrDefault(room => room.name == roomname) == null)
                    {
                        Debug.Log("VC_CreateRoom: " + roomname);
                        MonobitNetwork.CreateRoom(roomname);
                    }
                    else
                    {
                        Debug.Log("VC_JoinRoom: " + roomname);
                        MonobitNetwork.JoinRoom(roomname);
                    }
                    yield return new WaitForSecondsRealtime(1.0f);
                    Debug.Log(string.Join(" / ", MonobitNetwork.GetRoomData().Select(x => x.name)));
                    StartCoroutine(WaitLoginCoroutine(playerName, roomname));
                }
            }
            else
            {
                MonobitNetwork.DisconnectServer();
                MonobitNetwork.playerName = playerName;
                MonobitNetwork.autoJoinLobby = true;
                MonobitNetwork.ConnectServer("SimpleVoiceChat_v1.0");
                Debug.Log("VC_WaitConnected...");
                yield return new WaitForSecondsRealtime(1.0f);
                StartCoroutine(WaitConnectedCoroutine(playerName, roomname));
            }
        }
        /// <summary>
        /// ボイスチャットルームへの入室待機処理
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitLoginCoroutine(string playerName, string roomname)
        {
            yield return null;
            yield return new WaitForSecondsRealtime(1.0f);
            if (MonobitNetwork.inRoom)
            {
                if (myVoice != null)
                {
                    myVoice.SendStreamType = StreamType.BROADCAST;
                }
            }
            else
            {
                Debug.Log("VC_RetryLogin...");
                StartCoroutine(WaitConnectedCoroutine(playerName, roomname));
            }
        }

        // 自身がルーム入室に成功したときの処理
        public void OnJoinedRoom()
        {
            Debug.Log("VC_OnJoinedRoom!");

            vcPlayerInfo.Clear();
            vcPlayerInfo.Add(MonobitNetwork.player, (Int32)EnableVC.DISABLE);

            foreach (MonobitPlayer player in MonobitNetwork.otherPlayersList)
            {
                vcPlayerInfo.Add(player, (Int32)EnableVC.ENABLE);
            }

            GameObject go = MonobitNetwork.Instantiate("VoiceActor", Vector3.zero, Quaternion.identity, 0);
            myVoice = go.GetComponent<MonobitVoice>();
			if (myVoice != null)
			{
	            myVoice.SetMicrophoneErrorHandler(OnMicrophoneError);
	            myVoice.SetMicrophoneRestartHandler(OnMicrophoneRestart);
			}
        }

        // 誰かがルームにログインしたときの処理
        public void OnOtherPlayerConnected(MonobitPlayer newPlayer)
        {
            if (!vcPlayerInfo.ContainsKey(newPlayer))
            {
                vcPlayerInfo.Add(newPlayer, (Int32)EnableVC.ENABLE);
            }
        }

        // 誰かがルームからログアウトしたときの処理
        public virtual void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
        {
            if (vcPlayerInfo.ContainsKey(otherPlayer))
            {
                vcPlayerInfo.Remove(otherPlayer);
            }
        }

		/// <summary>
		/// マイクのエラーハンドリング用デリゲート
		/// </summary>
		/// <returns>
		/// true : 内部にてStopCaptureを実行しループを抜けます。
		/// false: StopCaptureを実行せずにループを抜けます。
		/// </returns>
		public bool OnMicrophoneError()
		{
			UnityEngine.Debug.LogError("Error: Microphone Error !!!");
			return true;
		}

		/// <summary>
		/// マイクのリスタート用デリゲート
		/// </summary>
		/// <remarks>
		/// 呼び出された時点ではすでにStopCaptureされています。
		/// </remarks>
		public void OnMicrophoneRestart()
		{
			UnityEngine.Debug.LogWarning("Info: Microphone Restart !!!");
		}
    }
}
