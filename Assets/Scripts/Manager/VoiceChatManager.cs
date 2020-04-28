using Assets.Scripts.Service;
using MonobitEngine.Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceChatManager : VoiceChatService
{
    private void OnDestroy()
    {
        StopVoiceChat();
    }
}
