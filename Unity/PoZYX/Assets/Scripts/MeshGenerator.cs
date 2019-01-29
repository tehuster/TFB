using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public GameObject[] corners;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3 (corners[0].transform.position.x,0.1f,corners[0].transform.position.z),
            new Vector3 (corners[3].transform.position.x,0.1f,corners[3].transform.position.z),
            new Vector3 (corners[1].transform.position.x,0.1f,corners[1].transform.position.z),
            new Vector3 (corners[2].transform.position.x,0.1f,corners[2].transform.position.z)
        };

        triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
