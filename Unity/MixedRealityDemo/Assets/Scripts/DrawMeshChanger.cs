using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class DrawMeshChanger : MonoBehaviour {
    GestureRecognizer recognizer;

    public SpatialMapping mapping;

    // Use this for initialization
    void Start () {
        Debug.Log("DrawMeshChanger.Start");

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update () {
	
	}

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Debug.Log(string.Format("Recognizer_TappedEvent : {0}", mapping.DrawVisualMeshes));

        mapping.DrawVisualMeshes = !mapping.DrawVisualMeshes;
    }
}
