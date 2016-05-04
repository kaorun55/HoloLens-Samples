using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraTexture : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        var devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            text.text = "Webカメラが検出できませんでした。";
            return;
        }

        text.text = string.Format( "Device Count : {0}", devices.Length);
        foreach (var device in devices)
        {
            text.text += string.Format("\n{0}", device.name);
        }

        // WebCamテクスチャを作成する
        var webcamTexture = new WebCamTexture( devices[0].name );
        GetComponent<Renderer>().material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
