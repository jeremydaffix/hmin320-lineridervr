using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{
    public List<Transform> ControlPoints;
    public int PointsCount;

    protected LineRenderer LineRenderer;
    public LineRenderer ControlPointsLineRenderer;

    public bool UseCasteljau;
    public bool UseJustPoints;

    private void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    Vector3 GetPointCasteljau(Vector3[] controlPoints, float u)
    {
        Vector3[] points = new Vector3[controlPoints.Length-1];
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

    void FixedUpdate ()
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

        Vector3[] controlPointsPositions = new Vector3[ControlPoints.Count];
        for (int i = 0; i < ControlPoints.Count; i++)
        {
            controlPointsPositions[i] = ControlPoints[i].position;
        }

        //var watch = System.Diagnostics.Stopwatch.StartNew();
        
        Vector3[] points = new Vector3[PointsCount];
        for (int i = 0; i < PointsCount; i++)
        {
            float u = i * (1.0f / (PointsCount - 1));
            if (UseCasteljau)
            {
                points[i] = GetPointCasteljau(controlPointsPositions, u);
            }
            /*else if (UseJustPoints)
            {
                points[i] = controlPointsPositions[i];
            }*/
            else
            {
                points[i] = GetPointBernstein(u);
            }

            Vector3 tangent = GetTangent(controlPointsPositions, u);
            Debug.DrawLine(points[i], points[i] + tangent, Color.red);
            Vector3 binormal = Vector3.Cross(Vector3.up, tangent).normalized;
            Debug.DrawLine(points[i], points[i] + binormal, Color.green);
            Vector3 normal = Vector3.Cross(tangent, binormal);
            Debug.DrawLine(points[i], points[i] + normal, Color.cyan);
        }

        //watch.Stop();
        //var elapsedMs = watch.ElapsedTicks;
        //Debug.Log("Curve generated in " + elapsedMs + " ticks.");

        LineRenderer.positionCount = PointsCount;
        LineRenderer.SetPositions(points);

        ControlPointsLineRenderer.positionCount = ControlPoints.Count;
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
