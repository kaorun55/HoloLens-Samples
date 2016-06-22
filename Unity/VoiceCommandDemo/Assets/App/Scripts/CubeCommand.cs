using UnityEngine;

public class CubeCommand : MonoBehaviour
{
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
