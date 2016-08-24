using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VR.WSA.Input;

public interface IManipulationReciever : IEventSystemHandler
{
    void OnManipulationStarted(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay);
    void OnManipulationUpdated(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay);
    void OnManipulationCompleted(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay);
    void OnManipulationCanceled(InteractionSourceKind source, Vector3 cumulativeDelta, Ray headRay);
}
