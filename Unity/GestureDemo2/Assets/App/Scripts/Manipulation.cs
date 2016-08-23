using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using System;
using HoloToolkit.Unity;

public class Manipulation : MonoBehaviour {
    private GestureRecognizer gestureRecognizer;

    Vector3 oldPosition = Vector3.zero;

    // Use this for initialization
    void Start () {
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);
        gestureRecognizer.ManipulationStartedEvent += GestureRecognizer_ManipulationStartedEvent;
        gestureRecognizer.ManipulationUpdatedEvent += GestureRecognizer_ManipulationUpdatedEvent;
        gestureRecognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnSelect()
    {
    }

    private void GestureRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        //Debug.Log("Manipulation Updated : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z ));
        transform.Translate(cumulativeDelta - oldPosition);

        oldPosition = cumulativeDelta;
    }

    private void GestureRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        //Debug.Log("Manipulation Started : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z));
        oldPosition = cumulativeDelta;
    }
}

