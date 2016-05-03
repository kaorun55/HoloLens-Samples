using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraParameter : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        var position = string.Format("{0:0.000}, {1:0.000}, {2:0.000}", transform.position.x, transform.position.y, transform.position.z);
        var rotation = string.Format("{0:0.0}, {1:0.0}, {2:0.0}", transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        text.text = string.Format("{0}\n{1}", position, rotation);

    }
}
