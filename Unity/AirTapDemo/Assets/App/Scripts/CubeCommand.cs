using UnityEngine;
using System.Collections;

public class CubeCommand : MonoBehaviour {

    public Material first;
    public Material second;

    public void OnSelect()
    {
        Debug.Log("OnSelect");

        var material = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = (material.color == first.color) ? second : first;
    }
}
