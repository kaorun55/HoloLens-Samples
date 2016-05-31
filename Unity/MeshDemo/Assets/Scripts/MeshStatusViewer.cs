using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.VR.WSA.Input;
using UnityEngine.VR.WSA;
using System;

public class MeshStatusViewer : MonoBehaviour {
    SpatialMapping mapping;
    GestureRecognizer recognizer;

    public Text text;

    public GameObject[] cubes;

    // Use this for initialization
    void Start () {
        mapping = gameObject.GetComponent<SpatialMapping>();

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.StartCapturingGestures();

        StartCoroutine( UpdateMesh() );
    }

    private IEnumerator UpdateMesh()
    {
        while (true)
        {
            Vector3 min = Vector3.zero;
            Vector3 max = Vector3.zero;

            var filters = mapping.GetMeshFilters();

            text.text = "";
            text.text += string.Format("Mesh Count  : {0}\n", filters.Count);

            foreach (var filter in filters)
            {
                
                var mesh = filter.sharedMesh;

                foreach (var vertex in mesh.vertices)
                {
                    // ローカル座標をワールド座標に変換する
                    var v = filter.transform.TransformPoint(vertex);

                    // 最小、最大値を取得
                    min.x = Mathf.Min(v.x, min.x);
                    min.y = Mathf.Min(v.y, min.y);
                    min.z = Mathf.Min(v.z, min.z);

                    max.x = Mathf.Max(v.x, max.x);
                    max.y = Mathf.Max(v.y, max.y);
                    max.z = Mathf.Max(v.z, max.z);
                }
            }

            text.text += string.Format("Min Point  : {0},{1},{2}\n", min.x, min.y, min.z);
            text.text += string.Format("Max Point  : {0},{1},{2}\n", max.x, max.y, max.z);

            cubes[0].transform.localPosition = new Vector3(min.x, min.y, min.z);
            cubes[1].transform.localPosition = new Vector3(min.x, min.y, max.z);
            cubes[2].transform.localPosition = new Vector3(max.x, min.y, max.z);
            cubes[3].transform.localPosition = new Vector3(max.x, min.y, min.z);

            cubes[4].transform.localPosition = new Vector3(min.x, max.y, min.z);
            cubes[5].transform.localPosition = new Vector3(min.x, max.y, max.z);
            cubes[6].transform.localPosition = new Vector3(max.x, max.y, max.z);
            cubes[7].transform.localPosition = new Vector3(max.x, max.y, min.z);

            yield return new WaitForSeconds(1);
        }
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        mapping.DrawVisualMeshes = !mapping.DrawVisualMeshes;
    }

    // Update is called once per frame
    void Update () {
    }
}
