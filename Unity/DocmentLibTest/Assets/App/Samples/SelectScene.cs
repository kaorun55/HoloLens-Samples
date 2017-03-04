using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR.WSA.Input;

public class SelectScene : MonoBehaviour {
    GestureRecognizer gesture;

    string AsycnAwaitTestScene = "AsycnAwaitTest";
    string DocumentLibraryTestScene = "DocumentLibraryTest";

    // Use this for initialization
    void Start () {
        GetComponent<TextMesh>().text = SceneManager.GetActiveScene().name;

        gesture = new GestureRecognizer();
        gesture.SetRecognizableGestures(GestureSettings.Tap);
        gesture.TappedEvent += Gesture_TappedEvent;
        gesture.StartCapturingGestures();
    }

    private void Gesture_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (SceneManager.GetActiveScene().name == AsycnAwaitTestScene)
        {
            SceneManager.LoadScene(DocumentLibraryTestScene);
        }
        else if (SceneManager.GetActiveScene().name == DocumentLibraryTestScene)
        {
            SceneManager.LoadScene(AsycnAwaitTestScene);
        }
    }
}
