using UnityEngine;
using System.Collections;
using System;

public class CubeCreator : MonoBehaviour {
    public GameObject cubePrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(CreateCube());
	}

    // Update is called once per frame
    void Update () {
	
	}

    private IEnumerator CreateCube()
    {
        while (true)
        {
            // カメラの正面から落とす
            float r = 1.5f;
            var theta = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            var x = r * Mathf.Sin(theta);
            var z = r * Mathf.Cos(theta);

            Instantiate(cubePrefab,
                new Vector3(x, 1, z),
                Quaternion.Euler(0, transform.rotation.eulerAngles.y, z));

            yield return new WaitForSeconds(1);
        }
    }
}
