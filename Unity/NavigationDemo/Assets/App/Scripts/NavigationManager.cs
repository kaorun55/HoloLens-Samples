using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using HoloToolkit.Unity;

public class NavigationManager : Singleton<NavigationManager> {

    private GestureRecognizer gestureRecognizer;

    // Use this for initialization
    void Start () {
        gestureRecognizer = new GestureRecognizer();

        var navigation = GestureSettings.NavigationX | GestureSettings.NavigationY | GestureSettings.NavigationRailsZ;
        var navigationRails = GestureSettings.NavigationRailsX | GestureSettings.NavigationRailsY | GestureSettings.NavigationRailsZ;

        gestureRecognizer.SetRecognizableGestures(navigationRails);

        gestureRecognizer.NavigationStartedEvent += GestureRecognizer_NavigationStartedEvent;
        gestureRecognizer.NavigationUpdatedEvent += GestureRecognizer_NavigationUpdatedEvent;
        gestureRecognizer.NavigationCompletedEvent += GestureRecognizer_NavigationCompletedEvent;
        gestureRecognizer.NavigationCanceledEvent += GestureRecognizer_NavigationCanceledEvent;

        gestureRecognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GestureRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log(string.Format("Navigation Started : {0},{1},{2}", normalizedOffset.x, normalizedOffset.y, normalizedOffset.z));
    }

    private void GestureRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log(string.Format("Navigation Updated : {0},{1},{2}", normalizedOffset.x, normalizedOffset.y, normalizedOffset.z));
    }

    private void GestureRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log(string.Format("Navigation Completed : {0},{1},{2}", normalizedOffset.x, normalizedOffset.y, normalizedOffset.z));
    }

    private void GestureRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log(string.Format("Navigation Canceled : {0},{1},{2}", normalizedOffset.x, normalizedOffset.y, normalizedOffset.z));
    }
}

