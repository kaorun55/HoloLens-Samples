using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity;


public class VoiceCommandManager : MonoBehaviour {
    KeywordRecognizer keywordRecognizer = null;

    public List<VoiceCommand> SendMessageCommand;
    public List<VoiceCommand> BroadcastMessageCommand;

    // Use this for initialization
    void Start () {
        Debug.Log("VoiceCommandManager Start.");

        // コマンドリストに変える
        var keywords = new List<string>();
        keywords.AddRange(SendMessageCommand.Select(c => c.Command));
        keywords.AddRange(BroadcastMessageCommand.Select(c => c.Command));

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);

        // 個別コマンド
        var sendCommand = SendMessageCommand.FirstOrDefault(v => v.Command == args.text);
        if (sendCommand != null)
        {
            SendVoiceCommand(sendCommand.MethodName);
        }

        // ブロードキャストコマンド
        var broadcastCommand = BroadcastMessageCommand.FirstOrDefault(v => v.Command == args.text);
        if (broadcastCommand != null)
        {
            BroadcastVoiceCommand(broadcastCommand.MethodName);
        }
    }

    void SendVoiceCommand(string methodName)
    {
        var focusObject = GazeManager.Instance.FocusedObject;
        if (focusObject != null)
        {
            focusObject.SendMessage(methodName);
        }
    }

    void BroadcastVoiceCommand(string methodName)
    {
        BroadcastMessage(methodName);
    }
}
