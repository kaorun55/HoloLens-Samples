using UnityEngine;
using System.Collections;

public class CubeCommand : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRed()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnBlue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public void OnGreen()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public void OnReset()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
