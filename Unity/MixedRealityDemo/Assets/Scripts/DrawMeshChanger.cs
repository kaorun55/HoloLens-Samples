using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class DrawMeshChanger : MonoBehaviour
{
    GestureRecognizer recognizer;

    public SpatialMapping mapping;

    public bool isWireframe = true;
    public Material Wireframe;
    public Material Occlusion;

    public Text text;

    // Use this for initialization
    void Start()
    {
        Debug.Log("DrawMeshChanger.Start");

        // エラータップジェスチャーを認識させる
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = mapping.IsMeshCreated ? "" : "Now Mesh Generating...";
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Debug.Log(string.Format("Recognizer_TappedEvent : {0}", isWireframe));

        // マテリアルを変更する
        mapping.SetMaterial(isWireframe ? Occlusion : Wireframe);
        isWireframe = !isWireframe;
    }
}
