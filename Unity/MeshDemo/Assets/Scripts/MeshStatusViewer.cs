using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.VR.WSA.Input;
using UnityEngine.VR.WSA;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Linq;

#if !UNITY_EDITOR
using Windows.Storage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
#endif

public class MeshStatusViewer : MonoBehaviour {
    SpatialMapping mapping;
    GestureRecognizer recognizer;

    public Text text;

    public GameObject[] cubes;

    bool isSaveMesh = false;

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

            yield return SaveMesh();

            yield return new WaitForSeconds(1);
        }
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        mapping.DrawVisualMeshes = !mapping.DrawVisualMeshes;

        isSaveMesh = true;
    }

    // Update is called once per frame
    void Update () {
    }

    IEnumerator SaveMesh()
    {
        if (!isSaveMesh)
        {
            yield break;
        }

        isSaveMesh = false;

        string modelData = "";

        int offset = 0;
        int count = 0;
        var filters = mapping.GetMeshFilters();

#if false
        var filter = filters.First();
#else
        foreach (var filter in filters)
#endif
        {
            if (filter != null)
            {
                // OBJ形式で出力する
                var mesh = filter.sharedMesh;

                modelData += string.Format("o object.{0}\n", ++count);

                foreach (var vertex in mesh.vertices)
                {
                    // ローカル座標をワールド座標に変換する
                    var v = filter.transform.TransformPoint(vertex);
                    modelData += string.Format("v {0} {1} {2}\n", v.x, v.y, v.z);
                }

                modelData += "\n";

                // 面を書く(これがうまくできてないらしい)
                for (int i = 0; i < mesh.triangles.Length; i += 3)
                {
                    modelData += string.Format("f {0} {1} {2}\n",
                        mesh.triangles[i + 0] + 1 + offset,
                        mesh.triangles[i + 1] + 1 + offset,
                        mesh.triangles[i + 2] + 1 + offset);
                }

                modelData += "\n";
                modelData += "\n";

                offset += mesh.vertexCount;
            }
        }

#if !UNITY_EDITOR
            var ao = KnownFolders.CameraRoll.CreateFileAsync("mapping.obj", CreationCollisionOption.GenerateUniqueName);
            ao.Completed = async delegate {
                if (ao.Status == Windows.Foundation.AsyncStatus.Completed)
                {
                    var file = ao.GetResults();

                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        var imageBuffer = Encoding.ASCII.GetBytes(modelData);
                        await stream.WriteAsync(imageBuffer.AsBuffer());
                    }

                    Debug.Log("write comlete");
                }
                else if(ao.Status == Windows.Foundation.AsyncStatus.Error)
                {
                     Debug.Log(ao.ErrorCode.Message);
                }
            };

            while (ao.Status == Windows.Foundation.AsyncStatus.Started)
            {
                yield return new WaitForEndOfFrame();
            }
#endif
    }
}
