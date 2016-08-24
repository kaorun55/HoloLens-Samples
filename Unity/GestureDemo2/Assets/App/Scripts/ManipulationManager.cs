using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using System;
using HoloToolkit.Unity;
using UnityEngine.EventSystems;

public class ManipulationManager : Singleton<ManipulationManager> {
    private GestureRecognizer gestureRecognizer;

    private GameObject focusedObject = null;

    // Use this for initialization
    void Start () {
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

        gestureRecognizer.ManipulationStartedEvent += GestureRecognizer_ManipulationStartedEvent;
        gestureRecognizer.ManipulationUpdatedEvent += GestureRecognizer_ManipulationUpdatedEvent;
        gestureRecognizer.ManipulationCompletedEvent += GestureRecognizer_ManipulationCompletedEvent;
        gestureRecognizer.ManipulationCanceledEvent += GestureRecognizer_ManipulationCanceledEvent;

        gestureRecognizer.StartCapturingGestures();
    }

    private void GestureRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        // 
        if (GestureManager.Instance.FocusedObject != null)
        {
            if (!ExecuteEvents.CanHandleEvent<IManipulationReciever>(GestureManager.Instance.FocusedObject))
            {
                return;
            }

            focusedObject = GestureManager.Instance.FocusedObject;

            ExecuteEvents.Execute<IManipulationReciever>(focusedObject, null, (reciever, e) => reciever.OnManipulationStarted(source, cumulativeDelta, headRay));
        }
    }

    private void GestureRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if (focusedObject != null)
        {
            ExecuteEvents.Execute<IManipulationReciever>(focusedObject, null, (reciever, e) => reciever.OnManipulationUpdated(source, cumulativeDelta, headRay));
        }
    }

    private void GestureRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if (focusedObject != null)
        {
            ExecuteEvents.Execute<IManipulationReciever>(focusedObject, null, (reciever, e) => reciever.OnManipulationCompleted(source, cumulativeDelta, headRay));
            focusedObject = null;
        }
    }

    private void GestureRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if (focusedObject != null)
        {
            ExecuteEvents.Execute<IManipulationReciever>(focusedObject, null, (reciever, e) => reciever.OnManipulationCanceled(source, cumulativeDelta, headRay));
            focusedObject = null;
        }
    }
}

