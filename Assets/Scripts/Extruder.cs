
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Extruder : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _vertexNew;
    private Vector3[] _vertexOld;
    private int[] _triangles ;
    private RaycastHit hit;
    private Camera _camera;
    private void Awake()
    {
        _mesh = GetComponent<MeshFilter>().sharedMesh;
        _camera = Camera.main;
        _vertexOld = _mesh.vertices;
        _vertexNew = _mesh.vertices;
        _triangles = _mesh.triangles;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Extrude(hit.triangleIndex);
            }
        }
    }

    private void Extrude(int index)
    {
        var _index0 = _triangles[index * 3 + 0];
        var _index1 = _triangles[index * 3 + 1];
        var _index2 = _triangles[index * 3 + 2];
        _vertexNew[_index0] += new Vector3(0, Random.Range(-2,2), 0);
        _vertexNew[_index1] += new Vector3(0, Random.Range(-2,2), 0);
        _vertexNew[_index2] += new Vector3(0, Random.Range(-2,2), 0);
        _mesh.vertices = _vertexNew;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
    }

    private void OnApplicationQuit()
    {
        _mesh.vertices = _vertexOld;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
    }
}
