using UnityEngine;
using System.Collections;
using System;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity;

public class Manipulation : MonoBehaviour, IManipulationReciever {

    Vector3 oldPosition = Vector3.zero;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnManipulationStarted(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        //Debug.Log("Manipulation Started : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z));

        oldPosition = cumulativeDelta;
    }

    public void OnManipulationUpdated(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
        //Debug.Log("Manipulation Updated : " + string.Format("{0},{1},{2}", cumulativeDelta.x, cumulativeDelta.y, cumulativeDelta.z ));

        transform.Translate(cumulativeDelta - oldPosition);
        oldPosition = cumulativeDelta;
    }

    public void OnManipulationCompleted(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
    }

    public void OnManipulationCanceled(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay)
    {
    }
}
