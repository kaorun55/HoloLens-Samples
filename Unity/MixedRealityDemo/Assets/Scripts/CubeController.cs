using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}
