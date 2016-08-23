using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using System;
using HoloToolkit.Unity;

public class Manipulation : MonoBehaviour {
    private GestureRecognizer gestureRecognizer;

    Vector3 oldPosition = Vector3.zero;

    bool isHold = false;

    // Use this for initialization
    void Start () {
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate | GestureSettings.Hold);
        gestureRecognizer.ManipulationStartedEvent += GestureRecognizer_ManipulationStartedEvent;
        gestureRecognizer.ManipulationUpdatedEvent += GestureRecognizer_ManipulationUpdatedEvent;
        gestureRecognizer.ManipulationCompletedEvent += GestureRecognizer_ManipulationCompletedEvent;
        gestureRecognizer.ManipulationCanceledEvent += GestureRecognizer_ManipulationCompletedEvent;

        gestureRecognizer.StartCapturingGestures();
    }

    private void GestureRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        isHold = false;
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
        if (isHold)
        {
            //Debug.Log("Manipulation Updated : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z ));
            transform.Translate(cumulativeDelta - oldPosition);

            oldPosition = cumulativeDelta;
        }
    }

    private void GestureRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        //Debug.Log("Manipulation Started : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z));
        Debug.Log("Manipulation Started : " + gameObject.name);

        if (GestureManager.Instance.FocusedObject == gameObject)
        {
            isHold = true;

            oldPosition = cumulativeDelta;
        }
    }
}

