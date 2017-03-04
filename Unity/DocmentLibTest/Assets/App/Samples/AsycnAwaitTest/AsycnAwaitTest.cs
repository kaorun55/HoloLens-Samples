using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_UWP
using System.Threading.Tasks;
#endif

public class AsycnAwaitTest : MonoBehaviour {
    public TextMesh textMeshAynsc;
    public TextMesh textMeshCoroutine;

    // Use this for initialization
    void Start() {
#if UNITY_UWP
        // Task.Runでasyncする
        Task.Run(async () => {
            UnityEngine.WSA.Application.InvokeOnAppThread(()=>{
                textMeshAynsc.text = "Task.Run before : " + Time.time;
            }, true);

            await Task.Delay(5000);

            UnityEngine.WSA.Application.InvokeOnAppThread(() => {
                textMeshAynsc.text = "Task.Run after  : " + Time.time;
            }, true);
        });

        StartCoroutine(HeavyTask());
#endif
    }

    private IEnumerator HeavyTask()
    {
#if UNITY_UWP
        // WaitWhileでまつ
        textMeshCoroutine.text = "Task.Run before : " + Time.time;

        var task = Task.Delay(5000);
        yield return new WaitWhile( () => !task.IsCompleted);

        textMeshCoroutine.text = "Task.Run after  : " + Time.time;
#else
        yield return null;
#endif
    }
}
