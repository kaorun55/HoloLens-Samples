using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class DrawMeshChanger : MonoBehaviour
{
    public SpatialMapping mapping;

    public bool isWireframe = true;
    public Material Wireframe;
    public Material Occlusion;

    public Text text;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text.text = mapping.IsMeshCreated ? "" : "Now Mesh Generating...";
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        // マテリアルを変更する
        ChangeMeshMaterial();
    }

    public void ChangeMeshMaterial()
    {
        mapping.SetMaterial(isWireframe ? Occlusion : Wireframe);
        isWireframe = !isWireframe;
    }
}
