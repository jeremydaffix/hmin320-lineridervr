using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public List<Transform> ControlPoints;
    public int PointsCount;

    protected LineRenderer LineRenderer;
    public LineRenderer ControlPointsLineRenderer;

    private void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate ()
    {
        Vector3[] points = new Vector3[PointsCount];
        for (int i = 0; i < PointsCount; i++)
        {
            float u = i * (1.0f / (PointsCount - 1));
            Vector3 point = new Vector3(0.0f, 0.0f, 0.0f);
            for (int k = 0; k < ControlPoints.Count; k++)
            {
                float b = Bernstein_polynomial(k, ControlPoints.Count - 1, u);
                point.x += b * ControlPoints[k].position.x;
                point.y += b * ControlPoints[k].position.y;
                point.z += b * ControlPoints[k].position.z;
            }
            points[i] = point;
        }

        LineRenderer.positionCount = PointsCount;
        LineRenderer.SetPositions(points);

        ControlPointsLineRenderer.positionCount = ControlPoints.Count;
        Vector3[] controlPointsPositions = new Vector3[ControlPoints.Count];
        for (int i = 0; i < ControlPoints.Count; i++)
        {
            controlPointsPositions[i] = ControlPoints[i].position;
        }
        ControlPointsLineRenderer.SetPositions(controlPointsPositions);
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
