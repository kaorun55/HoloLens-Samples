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

    bool isSendMesh = false;

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

            SendMesh();

#if !UNITY_EDITOR
            var ao = KnownFolders.PicturesLibrary.CreateFileAsync("mapping.jpg", CreationCollisionOption.ReplaceExisting);
            ao.Completed = async delegate {
                if (ao.Status == Windows.Foundation.AsyncStatus.Completed)
                {
                    var file = ao.GetResults();
                    //Debug.Log(file.Name);

                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        var imageBuffer = new byte[stream.Size];
                        await stream.WriteAsync(imageBuffer.AsBuffer());
                    }

                    //Debug.Log("write comlete");
                }
                else if(ao.Status == Windows.Foundation.AsyncStatus.Error)
                {
                     Debug.Log(ao.ErrorCode.Message);
                }
            };

            while (ao.Status == Windows.Foundation.AsyncStatus.Started)   // …… （3）
            {
                yield return new WaitForEndOfFrame();
            }

#endif

            yield return new WaitForSeconds(1);
        }
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        mapping.DrawVisualMeshes = !mapping.DrawVisualMeshes;

        isSendMesh = true;
    }

    // Update is called once per frame
    void Update () {
    }

    void SendMesh()
    {
        if (!isSendMesh)
        {
            return;
        }

        isSendMesh = false;

        string vertexData = "";
        string triangleData = "";

        var filters = mapping.GetMeshFilters();
        int offset = 0;
        //var filter = filters.First();
        foreach (var filter in filters)
        {
            if (filter != null)
            {
                var mesh = filter.sharedMesh;

                foreach (var vertex in mesh.vertices)
                {
                    // ローカル座標をワールド座標に変換する
                    var v = filter.transform.TransformPoint(vertex);
                    vertexData += string.Format("v {0} {1} {2}\n", v.x, v.y, v.z);
                }

                for (int i = 0; i < mesh.triangles.Length; i += 3)
                {
                    triangleData += string.Format("f {0} {1} {2}\n",
                        mesh.triangles[i + 0] + offset, mesh.triangles[i + 1] + offset, mesh.triangles[i + 2] + offset);
                }

                offset = mesh.vertices.Length;
            }
        }

        vertexData += triangleData;


        Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        SocketAsyncEventArgs args = new SocketAsyncEventArgs();
        args.RemoteEndPoint = new IPEndPoint(IPAddress.Parse("192.168.11.26"), 44444);
        args.Completed += (s, e) =>
        {
            if (e.ConnectSocket != null)
            {
                var data = Encoding.UTF8.GetBytes(vertexData);
                var length = BitConverter.GetBytes(data.Length);

                SocketAsyncEventArgs sendHeaderArgs = new SocketAsyncEventArgs();
                sendHeaderArgs.Completed += (ss, ee) => {
                    Debug.Log("Send Header");
                    SocketAsyncEventArgs sendDataArgs = new SocketAsyncEventArgs();
                    sendDataArgs.Completed += (sss, eee) => {
                        Debug.Log("Complete");
                    };

                    sendDataArgs.SetBuffer(data, 0, data.Length);
                    socket.SendAsync(sendDataArgs);
                };
                sendHeaderArgs.SetBuffer(length, 0, length.Length);
                socket.SendAsync(sendHeaderArgs);


                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Error");
            }
        };
        Debug.Log("Connect");
        socket.ConnectAsync(args);
    }
}
