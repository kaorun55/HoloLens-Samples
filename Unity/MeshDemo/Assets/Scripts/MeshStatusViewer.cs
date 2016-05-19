using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.VR.WSA.Input;
using UnityEngine.VR.WSA;

public class MeshStatusViewer : MonoBehaviour {
    SpatialMapping mapping;
    GestureRecognizer recognizer;

    public Text text;

    float duration = 0;

    public GameObject[] cubes;

    public Vector3 min = Vector3.zero;
    public Vector3 max = Vector3.zero;

    // Use this for initialization
    void Start () {
        mapping = gameObject.GetComponent<SpatialMapping>();
        mapping.OnUpdateMesh += Mapping_OnUpdateMesh;

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        mapping.DrawVisualMeshes = !mapping.DrawVisualMeshes;
    }

    private void Mapping_OnUpdateMesh(SpatialMapping mapping, SurfaceData bakedData)
    {
        text.text = "";
        text.text += string.Format("Mesh Count  : {0}\n", mapping.surfaces.Count);

        var filter = bakedData.outputMesh;
        foreach (var vertex in filter.mesh.vertices)
        {
            min.x = Mathf.Min(vertex.x, min.x);
            min.y = Mathf.Min(vertex.y, min.y);
            min.z = Mathf.Min(vertex.z, min.z);

            max.x = Mathf.Max(vertex.x, max.x);
            max.y = Mathf.Max(vertex.y, max.y);
            max.z = Mathf.Max(vertex.z, max.z);
        }

        text.text += string.Format("Min Point  : {0},{1},{2}\n", min.x, min.y, min.z);
        text.text += string.Format("Max Point  : {0},{1},{2}\n", max.x, max.y, max.z);

        var tree = ShowHierarchyTree.GetHierarchy();
        Debug.Log(tree);
        text.text += tree;

        cubes[0].transform.localPosition = new Vector3(min.x, min.y, min.z);
        cubes[1].transform.localPosition = new Vector3(min.x, min.y, max.z);
        cubes[2].transform.localPosition = new Vector3(max.x, min.y, max.z);
        cubes[3].transform.localPosition = new Vector3(max.x, min.y, min.z);

        cubes[4].transform.localPosition = new Vector3(min.x, max.y, min.z);
        cubes[5].transform.localPosition = new Vector3(min.x, max.y, max.z);
        cubes[6].transform.localPosition = new Vector3(max.x, max.y, max.z);
        cubes[7].transform.localPosition = new Vector3(max.x, max.y, min.z);
    }

    // Update is called once per frame
    void Update () {
    }
}
