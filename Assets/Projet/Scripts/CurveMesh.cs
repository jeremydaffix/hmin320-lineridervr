using UnityEngine;
using System.Collections.Generic;

public class CurveMesh : MonoBehaviour
{
    public List<Transform> ControlPoints;
    protected List<Vector3> ControlPointsPositions;
    public int PointsCount;
    private int LastPointsCount;

    protected LineRenderer LineRenderer;
    public LineRenderer ControlPointsLineRenderer;

    public bool UseCasteljau;
    public bool UseJustPoints;

    protected MeshFilter MeshFilterComponent;
    protected MeshCollider MeshColliderComponent;

    private void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        MeshFilterComponent = GetComponent<MeshFilter>();
        MeshColliderComponent = GetComponent<MeshCollider>();

        ControlPointsPositions = new List<Vector3>();
        Generate();
        LastPointsCount = PointsCount;
    }

    Vector3 GetPointCasteljau(Vector3[] controlPoints, float u)
    {
        Vector3[] points = new Vector3[controlPoints.Length - 1];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Vector3.Lerp(controlPoints[i], controlPoints[i + 1], u);
        }

        if (points.Length == 1)
        {
            return points[0];
        }
        else
        {
            return GetPointCasteljau(points, u);
        }
    }

    Vector3 GetPointBernstein(float u)
    {
        Vector3 point = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i = 0; i < ControlPoints.Count; i++)
        {
            float b = Bernstein_polynomial(i, ControlPoints.Count - 1, u);
            point.x += b * ControlPoints[i].position.x;
            point.y += b * ControlPoints[i].position.y;
            point.z += b * ControlPoints[i].position.z;
        }
        return point;
    }

    Vector3 GetTangent(Vector3[] controlPoints, float u)
    {
        Vector3[] points = new Vector3[controlPoints.Length - 1];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Vector3.Lerp(controlPoints[i], controlPoints[i + 1], u);
        }

        if (points.Length == 2)
        {
            return (points[0] - points[1]).normalized;
        }
        else
        {
            return GetTangent(points, u);
        }
    }

    void Generate()
    {
        //Debug.Log(PointsCount);
        if (ControlPoints.Count < 3)
        {
            LineRenderer.positionCount = 0;

            ControlPointsLineRenderer.positionCount = 0;

            return; ////
        }


        if (PointsCount <= 1)
        {
            PointsCount = 2;
        }


        if (ControlPointsPositions.Count == ControlPoints.Count)
        {
            bool controlPointsHasChanged = false;
            for (int i = 0; i < ControlPoints.Count; i++)
            {
                if (ControlPointsPositions[i] != ControlPoints[i].position)
                {
                    controlPointsHasChanged = true;
                    break;
                }
            }

            if (!controlPointsHasChanged)
            {
                if (LastPointsCount != PointsCount)
                {
                    LastPointsCount = PointsCount;
                }
                else
                {
                    return;
                }
            }
        }

        ControlPointsPositions.Clear();
        for (int i = 0; i < ControlPoints.Count; i++)
        {
            ControlPointsPositions.Add(ControlPoints[i].position);
        }

        var watch = System.Diagnostics.Stopwatch.StartNew();

        float edgeHeight = 0.15f;
        float edgeWidth = 0.15f;
        float roadWidth = 1.0f;

        int index = 0;

        int segments = PointsCount - 1;
        int triCount = segments * 7 * 2;
        int triIndexCount = triCount * 3;

        Vector3[] vertices = new Vector3[8 * PointsCount];
        Vector3[] normals = new Vector3[8 * PointsCount];
        Vector2[] uvs = new Vector2[8 * PointsCount];
        int[] triangleIndices = new int[triIndexCount];

        Vector3[] points = new Vector3[PointsCount];
        for (int i = 0; i < PointsCount; i++)
        {
            float u = i * (1.0f / (PointsCount - 1));
            if (UseCasteljau)
            {
                points[i] = GetPointCasteljau(ControlPointsPositions.ToArray(), u);
            }
            /*else if (UseJustPoints)
            {
                points[i] = controlPointsPositions[i];
            }*/
            else
            {
                points[i] = GetPointBernstein(u);
            }

            Vector3 tangent = GetTangent(ControlPointsPositions.ToArray(), u);
            Debug.DrawLine(points[i], points[i] + tangent, Color.red);
            Vector3 binormal = Vector3.Cross(Vector3.up, tangent).normalized;
            Debug.DrawLine(points[i], points[i] + binormal, Color.green);
            Vector3 normal = Vector3.Cross(tangent, binormal);
            Debug.DrawLine(points[i], points[i] + normal, Color.cyan);


            vertices[i * 8 + 0] = points[i] + binormal * (roadWidth / 2.0f + edgeWidth);
            normals[i * 8 + 0] = binormal;
            
            uvs[i * 8 + 0] = new Vector2(0.0f, 0.0f);
            vertices[i * 8 + 1] = points[i] + binormal * (roadWidth / 2.0f + edgeWidth) + normal * (edgeHeight);
            normals[i * 8 + 1] = (binormal + normal) / 2.0f;
            uvs[i * 8 + 1] = new Vector2(0.0f, 1.0f);
            vertices[i * 8 + 2] = points[i] + binormal * (roadWidth / 2.0f) + normal * edgeHeight;
            normals[i * 8 + 2] = (-binormal + normal) / 2.0f;
            uvs[i * 8 + 2] = new Vector2(0.0f, 1.0f);
            vertices[i * 8 + 3] = points[i] + binormal * (roadWidth / 2.0f);
            normals[i * 8 + 3] = (-binormal + normal) / 2.0f;
            uvs[i * 8 + 3] = new Vector2(0.0f, 0.0f);
            vertices[i * 8 + 4] = points[i] - binormal * (roadWidth / 2.0f);
            normals[i * 8 + 4] = (binormal + normal) / 2.0f;
            uvs[i * 8 + 4] = new Vector2(0.0f, 0.0f);
            vertices[i * 8 + 5] = points[i] - binormal * (roadWidth / 2.0f) + normal * edgeHeight;
            normals[i * 8 + 5] = (binormal + normal) / 2.0f;
            uvs[i * 8 + 5] = new Vector2(0.0f, 1.0f);
            vertices[i * 8 + 6] = points[i] - binormal * (roadWidth / 2.0f + edgeWidth) + normal * (edgeHeight);
            normals[i * 8 + 6] = (-binormal + normal) / 2.0f;
            uvs[i * 8 + 6] = new Vector2(0.0f, 1.0f);
            vertices[i * 8 + 7] = points[i] - binormal * (roadWidth / 2.0f + edgeWidth);
            normals[i * 8 + 7] = -binormal;
            uvs[i * 8 + 7] = new Vector2(0.0f, 0.0f);
            /*
            GameObject[] gameobjects = new GameObject[8];
            for (int v = 0; v < 8; v++)
            {
                gameobjects[v] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gameobjects[v].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                gameobjects[v].transform.position = vertices[i * 8 + v];
                gameobjects[v].name = (i * 8 + v).ToString();
                Debug.DrawLine(vertices[i * 8 + v], vertices[i * 8 + v] + normals[i * 8 + v], Color.magenta, 30.0f);
            }
            */
            if (i < PointsCount - 1)
            {
                for (int k = 0; k < (8 - 1); k++)
                {
                    // index = i * (8 - 1) * 3 * 2 + k * 3 * 2 + 0;
                    triangleIndices[index++] = i * 8 + k;
                    triangleIndices[index++] = (i + 1) * 8 + k;
                    triangleIndices[index++] = i * 8 + k + 1;

                    triangleIndices[index++] = i * 8 + k + 1;
                    triangleIndices[index++] = (i+1) * 8 + k;
                    triangleIndices[index++] = (i+1) * 8 + k + 1;
                }
            }
        }


        if (MeshFilterComponent.mesh == null)
            MeshFilterComponent.mesh = new Mesh();
        Mesh mesh = MeshFilterComponent.mesh;

        mesh.Clear();
        mesh.name = "CurveMesh";
        mesh.vertices = vertices;
        mesh.normals = normals;
        //mesh.uv = uvs;
        mesh.triangles = triangleIndices;

        MeshColliderComponent.sharedMesh = mesh;

        watch.Stop();
        var elapsedMs = watch.ElapsedTicks;
        Debug.Log("Curve generated in " + watch.ElapsedMilliseconds + "ms (" + watch.ElapsedTicks + " ticks).");

        LineRenderer.positionCount = PointsCount;
        LineRenderer.SetPositions(points);

        ControlPointsLineRenderer.positionCount = ControlPoints.Count;
        ControlPointsLineRenderer.SetPositions(ControlPointsPositions.ToArray());
    }

    public void FixedUpdate()
    {
        Generate();
    }

    float Factorial(float n)
    {
        float result = 1;
        for (int i = 0; i < n; i++)
        {
            result *= (n - i);
        }
        return result;
    }

    float Binomial_coefficient(float n, float k)
    {
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }

    float Bernstein_polynomial(float i, float n, float u)
    {
        return Binomial_coefficient(n, i) * Mathf.Pow(u, i) * Mathf.Pow(1.0f - u, n - i);
    }
}
