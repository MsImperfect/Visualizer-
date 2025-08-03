using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRingDrawer : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 100;
    public float lineWidth = 0.02f;

    void Start()
    {
        DrawOrbit();
    }

    void DrawOrbit()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.loop = true;
        line.positionCount = segments;
        line.widthMultiplier = lineWidth;

        // Set the orbit line color to black
        line.material = new Material(Shader.Find("Sprites/Default")); // Ensures material accepts color
        line.startColor = Color.black;
        line.endColor = Color.black;

        Vector3[] points = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;
            points[i] = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        }

        line.SetPositions(points);
    }
}
