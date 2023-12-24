using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainGeneration : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;
    Vector3[] vertices;
    int[] trinagles;

    [SerializeField] Texture2D texture;
    [SerializeField] int xSize = 20;
    [SerializeField] int zSize = 20;
    [SerializeField] int scale = 0;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for(int z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                Color c = texture.GetPixel(x,z);
                vertices[i] = new Vector3(x, c.r * scale, z); //c.r * scale
                i++;
            }
        }

        trinagles = new int[(xSize - 1) * (zSize - 1) * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0;z < zSize -1; z++)
        {
            for (int x = 0; x < xSize -1; x++)
            {
                trinagles[tris + 0] = vert + 0;
                trinagles[tris + 1] = vert + xSize + 1;
                trinagles[tris + 2] = vert + 1;
                trinagles[tris + 3] = vert + 1;
                trinagles[tris + 4] = vert + xSize + 1;
                trinagles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }  
    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = trinagles;
    }

    private void OnDrawGizmos()
    {
        if(vertices == null) return;
        for(int i = 0; i < vertices.Length;i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
