using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class GestureManager : MonoBehaviour {

    Material mat;
    GestureRecognizer recognizer;


    // Use this for initialization
    void Start () {
        mat = GetComponent<Renderer>().material;

        recognizer = new GestureRecognizer();
        //recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.HoldCanceledEvent += Recognizer_HoldCanceledEvent;
        recognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        recognizer.HoldCompletedEvent += Recognizer_HoldCompletedEvent;
        //recognizer.RecognitionStartedEvent += Recognizer_RecognitionStartedEvent;
        //recognizer.RecognitionEndedEvent += Recognizer_RecognitionEndedEvent;
        recognizer.StartCapturingGestures();
    }

    private void Recognizer_RecognitionStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        mat.color = Color.cyan;
    }

    private void Recognizer_RecognitionEndedEvent(InteractionSourceKind source, Ray headRay)
    {
        mat.color = Color.gray;
    }

    private void Recognizer_HoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        mat.color = Color.green;
    }

    private void Recognizer_HoldCompletedEvent(InteractionSourceKind source, Ray headRay)
    {
        mat.color = Color.yellow;
    }

    private void Recognizer_HoldCanceledEvent(InteractionSourceKind source, Ray headRay)
    {
        mat.color = Color.red;
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        mat.color = Color.blue;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mat.color = Color.red;
        }
	}
}
