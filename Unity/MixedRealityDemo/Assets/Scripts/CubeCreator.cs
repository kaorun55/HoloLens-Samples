using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using System;

public class CubeCreator : MonoBehaviour
{
    GestureRecognizer gestureRecognizer;

    public GameObject cubePrefab;

    // Use this for initialization
    void Start()
    {
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        gestureRecognizer.StartCapturingGestures();
    }

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        CreateCube();
    }

    private void CreateCube()
    {
        // カメラの正面から落とす
        float r = 1.5f;
        var theta = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        var x = r * Mathf.Sin(theta);
        var z = r * Mathf.Cos(theta);

        Instantiate(cubePrefab,
            new Vector3(x, 1, z),
            Quaternion.Euler(0, transform.rotation.eulerAngles.y, z));
    }
}
