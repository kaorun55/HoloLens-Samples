using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
using HoloToolkit.Unity;

public class VoiceCommand : MonoBehaviour {
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    Dictionary<string, string> keywordSendMessage = new Dictionary<string, string>();

    // Use this for initialization
    void Start () {
        Debug.Log("VoiceCommand Start.");

        keywords.Add("Reset", () =>
        {
            Debug.Log("Reset");

            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnReset");
        });

        // Helloが認識しやすい...
        keywords.Add("Hello", () =>
        {
            Debug.Log("Hello");

            var focusObject = GazeManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                focusObject.SendMessage("OnBlue");
            }
        });

        keywords.Add("Red", () =>
        {
            Debug.Log("Red");

            var focusObject = GazeManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                focusObject.SendMessage("OnRed");
            }
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
