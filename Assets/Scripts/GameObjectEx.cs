using UnityEngine;

namespace LD54
{
    public static class GameObjectEx
    {
        public static void DrawCircle(this GameObject container, float radius, float lineWidth, Color lineColor)
        {
            var segments = 360;
            LineRenderer line;
            if (!container.TryGetComponent<LineRenderer>(out line))
            {
                line = container.AddComponent<LineRenderer>();
            }

            line.useWorldSpace = false;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = segments + 1;
            line.startColor = lineColor;
            line.endColor = lineColor;

            var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
            var points = new Vector3[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                var rad = Mathf.Deg2Rad * (i * 360f / segments);
                points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
            }

            line.SetPositions(points);
        }
    }
}
