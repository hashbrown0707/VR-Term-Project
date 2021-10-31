using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDemo : MonoBehaviour
{
    public GameObject quad;

//    public float width;
//    public float height;
    private MeshFilter _meshFilter;

//    // Use this for initialization
    void Start()
    {
        _meshFilter = quad.GetComponent<MeshFilter>();
        var mesh = new Mesh();
        _meshFilter.mesh = mesh;

//        CreateQuadMesh(mesh);
//        CreateReticleVertices();
    }

//
//
//    public float kTwoPi = Mathf.PI;
//
//    public float Z = 1;
//    public float X = 1;
//
//    private void CreateReticleVertices()
//    {
//        Mesh mesh = new Mesh();
//        gameObject.AddComponent<MeshFilter>();
//        _meshFilter.mesh = mesh;
//
//        int segments_count = 100;
//        int vertex_count = (segments_count + 1) * 2;
//
//        #region Vertices
//
//        Vector3[] vertices = new Vector3[vertex_count];
//
//
//        int vi = 0;
//        for (int si = 0; si <= segments_count; ++si)
//        {
//            // Add two vertices for every circle segment: one at the beginning of the
//            // prism, and one at the end of the prism.
//            float angle = (float) si / (float) (segments_count) * kTwoPi;
//
//            float x = Mathf.Sin(angle);
//            float y = Mathf.Cos(angle);
//
//            vertices[vi++] = new Vector3(x * X, 0, y * Z); // Outer vertex.
//            vertices[vi++] = new Vector3(x * X, 1, y * Z); // Inner vertex.
//        }
//
//        #endregion
//
//        #region Triangles
//
//        int indices_count = (segments_count + 1) * 3 * 2;
//        int[] indices = new int[indices_count];
//
//        int vert = 0;
//        int idx = 0;
//        for (int si = 0; si < segments_count; ++si)
//        {
//            indices[idx++] = vert + 1;
//            indices[idx++] = vert;
//            indices[idx++] = vert + 2;
//
//            indices[idx++] = vert + 1;
//            indices[idx++] = vert + 2;
//            indices[idx++] = vert + 3;
//
//            vert += 2;
//        }
//
//        #endregion
//
//        mesh.vertices = vertices;
//        mesh.triangles = indices;
//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();
//    }
//
//    private void CreateQuadMesh(Mesh mesh)
//    {
//        Vector3[] vertices = new Vector3[4];
//
//        vertices[0] = new Vector3(0, 0, 0);
//        vertices[1] = new Vector3(width, 0, 0);
//        vertices[2] = new Vector3(0, height, 0);
//        vertices[3] = new Vector3(width, height, 0);
//
//        mesh.vertices = vertices;
//
//        int[] tri = new int[6];
//
//        tri[0] = 0;
//        tri[1] = 2;
//        tri[2] = 1;
//
//        tri[3] = 2;
//        tri[4] = 3;
//        tri[5] = 1;
//
//        mesh.triangles = tri;
//
////        Vector3[] normals = new Vector3[4];
////
////        normals[0] = -Vector3.forward;
////        normals[1] = -Vector3.forward;
////        normals[2] = -Vector3.forward;
////        normals[3] = -Vector3.forward;
////
////        mesh.normals = normals;
//
//        Vector2[] uv = new Vector2[4];
//
//        uv[0] = new Vector2(0, 0);
//        uv[1] = new Vector2(1, 0);
//        uv[2] = new Vector2(0, 1);
//        uv[3] = new Vector2(1, 1);
//
//        mesh.uv = uv;
//
//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();
//        mesh.RecalculateTangents();
//    }
//
//    // Update is called once per frame
    void Update()
    {
        _meshFilter.mesh = CreateArcSurface();
    }


//    public float planeCurvature = 1;
//
//    void CreatePlane()
//    {
//        Mesh mesh = new Mesh();
//        _meshFilter.mesh = mesh;
//
//        int segments_count = planesSeg;
//        int vertex_count = (segments_count + 1) * 2;
//
//        Vector3[] vertices = new Vector3[vertex_count];
//
//        int vi = 0;
//
//        float setup = planeWidth * 1.0f / segments_count;
//
//        for (int si = 0; si <= segments_count; si++)
//        {
//            float y = 0;
//
//            //float angle = (float) si / (float) (segments_count) * kTwoPi;
//
//            //float z = (float) (Mathf.Sin(angle) / planeWidth * 1.0 / 2);
//
//            float angle = (float) si / (float) (segments_count) * kTwoPi;
//
//            float z = Mathf.Sin(angle);
//            float x = Mathf.Cos(angle);
//
//            // Top
//            // 
////             x = -planeWidth / 2 + si * setup;
//
//            vertices[vi++] = new Vector3(x, planeHeight / 2 + y, z);
//
//            // Bottom
//            // -planeWidth / 2 + x
//            vertices[vi++] = new Vector3(x, -planeHeight / 2 + y, z);
//        }
//
//        int indices_count = (segments_count + 1) * 3 * 2;
//        int[] indices = new int[indices_count];
//
//        int vert = 0;
//        int idx = 0;
//        for (int si = 0; si < segments_count; si++)
//        {
//            indices[idx++] = vert + 1;
//            indices[idx++] = vert;
//            indices[idx++] = vert + 2;
//
//            indices[idx++] = vert + 1;
//            indices[idx++] = vert + 2;
//            indices[idx++] = vert + 3;
//
//            vert += 2;
//        }
//
//        mesh.vertices = vertices;
//        mesh.triangles = indices;
//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();
//        mesh.RecalculateTangents();
//    }

    [Range(-5, 5)] public float threshold;
    public float planeWidth = 1;
    public float planeHeight = 1;
    public int planesSeg = 64;

    private Mesh CreateArcSurface()
    {
        Mesh mesh = new Mesh();

        int segments_count = planesSeg;
        int vertex_count = (segments_count + 1) * 2;

        Vector3[] vertices = new Vector3[vertex_count];

        int vi = 0;

        // 普通平面步
        float widthSetup = planeWidth * 1.0f / segments_count;

        // 半径
        float r = planeWidth * 1.0f / (Mathf.Sin(threshold / 2) * 2);

        // 弧度步
        float angleSetup = threshold / planesSeg;

        // 余角
        float coangle = (Mathf.PI - threshold) / 2;

        // 弓形的高度
        // https://zh.wikipedia.org/wiki/%E5%BC%93%E5%BD%A2
        float h = r - (r * Mathf.Cos(threshold / 2));

        // 弓形高度差值（半径-高度）
        float diff = r - h;

        for (int si = 0; si <= segments_count; si++)
        {
            float x = 0;

            float z = 0;

            if (threshold == 0)
            {
                // 阈值为0时,按照普通平面设置顶点
                x = widthSetup * si;

                vertices[vi++] = new Vector3(-planeWidth / 2 + x, planeHeight / 2, z);

                vertices[vi++] = new Vector3(-planeWidth / 2 + x, -planeHeight / 2, z);
            }
            else
            {
                // 阈值不为0时,根据圆的几何性质计算弧上一点
                // https://zh.wikipedia.org/wiki/%E5%9C%86
                x = r * Mathf.Cos(coangle + angleSetup * si);
                z = r * Mathf.Sin(coangle + angleSetup * si);

                vertices[vi++] = new Vector3(-x, planeHeight / 2, z - diff);
                vertices[vi++] = new Vector3(-x, -planeHeight / 2, z - diff);
            }
        }

        int indices_count = segments_count * 3 * 2;
        int[] indices = new int[indices_count];

        int vert = 0;
        int idx = 0;
        for (int si = 0; si < segments_count; si++)
        {
            indices[idx++] = vert + 1;
            indices[idx++] = vert;
            indices[idx++] = vert + 3;

            indices[idx++] = vert;
            indices[idx++] = vert + 2;
            indices[idx++] = vert + 3;

            vert += 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = indices;

        // // https://answers.unity.com/questions/154324/how-do-uvs-work.html
        Vector2[] uv = new Vector2[vertices.Length];

        float uvSetup = 1.0f / segments_count;

        int iduv = 0;
        for (int i = 0; i < uv.Length; i = i + 2)
        {
            uv[i] = new Vector2(uvSetup * iduv, 1);
            uv[i + 1] = new Vector2(uvSetup * iduv, 0);
            iduv++;
        }

        mesh.uv = uv;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }
}