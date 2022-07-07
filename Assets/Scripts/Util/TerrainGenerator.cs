using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public bool generateMesh = false;
    public Texture2D heightMap;
    public int vWidth = 10, vHeight = 10;
    public MeshFilter meshHolder;
    public float vertDist = 1f;
    public float rangeMin;
    public float rangeMax;
    public float toRange = 3;


    // Update is called once per frame
    void Update()
    {
    }

    private void OnValidate()
    {
        if (generateMesh)
        {
            generateMesh = false;
            GenrateMesh();
        }
    }

    private void GenrateMesh()
    {
        var mesh = new Mesh();
        var vertexList = new List<Vector3>();
        mesh.GetVertices(vertexList);
        var triList = new List<int>();
        var posMap = new Dictionary<Tuple<int, int>, int>();
        mesh.GetTriangles(triList, 0);
        if (heightMap != null)
        {
            int height = heightMap.height;
            int width = heightMap.width;
            int xInc = height / vHeight;
            int yInc = width / vWidth;

            float mn = 2, mx = -1;

            for (int y = 0; y < height; y += yInc)
            {
                for (int x = 0; x < width; x += xInc)
                {
                    var z = heightMap.GetPixel(x, y).r;
                    mn = MathF.Min(mn, z);
                    mx = MathF.Max(mx, z);
                    z -= rangeMin;
                    z /= (rangeMax - rangeMin);
                    //z = 1 - z;
                    z *= toRange;
                    posMap.Add(new Tuple<int, int>(x, y), vertexList.Count);
                    //Debug.Log(x + " / " + y + " Added");
                    vertexList.Add(new Vector3(x / xInc * vertDist, z, y / yInc * vertDist));
                    if (y > 0 && x + xInc < width)
                    {
                        triList.Add(posMap[new Tuple<int, int>(x, y)]);
                        triList.Add(posMap[new Tuple<int, int>(x + xInc, y - yInc)]);
                        triList.Add(posMap[new Tuple<int, int>(x, y - yInc)]);
                    }
                    //Debug.Log("f added");
                    if (y > 0 && x > 0)
                    {
                        triList.Add(posMap[new Tuple<int, int>(x, y)]);
                        triList.Add(posMap[new Tuple<int, int>(x, y - yInc)]);
                        triList.Add(posMap[new Tuple<int, int>(x - xInc, y)]);
                    }
                    //Debug.Log("s added");
                }
            }
            //rangeMin = mn;
            //rangeMax = mx;
        }
        mesh.SetVertices(vertexList);
        mesh.SetTriangles(triList, 0);
        Vector2[] uvs = new Vector2[vertexList.Count];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertexList[i].x, vertexList[i].z);
        }
        mesh.uv = uvs;
        Debug.Log(vertexList[0]);
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        meshHolder.mesh = mesh;
    }
}
