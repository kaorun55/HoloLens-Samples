using UnityEngine;
using System.Collections;
using System;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity;

public class Manipulation : MonoBehaviour, IManipulationReciever {

    Vector3 oldPosition = Vector3.zero;

    bool isHold = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnManipulationStarted(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if (GestureManager.Instance.FocusedObject == gameObject)
        {
            //Debug.Log("Manipulation Started : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z));

            isHold = true;

            oldPosition = cumulativeDelta;
        }
    }

    public void OnManipulationUpdated(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        if (isHold)
        {
            //Debug.Log("Manipulation Updated : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z ));
            transform.Translate(cumulativeDelta - oldPosition);

            oldPosition = cumulativeDelta;
        }
    }

    public void OnManipulationCompleted(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        isHold = false;
    }

    public void OnManipulationCanceled(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        isHold = false;
    }
}
