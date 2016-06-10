using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System.Collections.Generic;

public class PlaneFindingDemo : MonoBehaviour {

    [Range(0, 45)]
    public float SnapToGravityThreshold = 0.0f;

    [Range(0, 10)]
    public float MinArea = 1.0f;

    public GameObject PlanePrefab;

    GameObject root;

    // Use this for initialization
    void Start () {
        root = new GameObject("root");
        root.transform.parent = transform;
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Transform cube in root.transform)
        {
            Destroy(cube.gameObject);
        }

        List<PlaneFinding.MeshData> meshData = new List<PlaneFinding.MeshData>();
        foreach (var mesh in SpatialMappingManager.Instance.GetMeshFilters())
        {
            meshData.Add(new PlaneFinding.MeshData(mesh));
        }

        Debug.Log("meshData : " + meshData.Count);

        var planes = PlaneFinding.FindSubPlanes(meshData, SnapToGravityThreshold);
        Debug.Log("planes : " + planes.Length);
        foreach (var plane in planes)
        {
            var cube = Instantiate(PlanePrefab);
            var p = cube.AddComponent<SurfacePlane>();
            p.Plane = plane;
        }
    }
}
