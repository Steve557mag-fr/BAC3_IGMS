using UnityEngine;

namespace COL1.Utilities {
    
    /* Utilitaire perso */

    public class MathUtils
    {
        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }
        public static float Remap(float start_1, float stop_1, float start_2, float stop_2, float value)
        {
            return start_2 + (stop_2 - start_2) * ((value - start_1) / (stop_1 - start_1));
        }

        public static float SnapValue(float value, float step = 0.5f)
        {
            return Mathf.Floor(value / step) * step;
        }
        public static Vector2 XY(Vector3 a)
        {
            return new(a.x, a.y);
        }
        public static Vector2 XZ(Vector3 a)
        {
            return new(a.x, a.z);
        }
        public static Vector2 ZW(Vector4 a)
        {
            return new(a.z, a.w);
        }
        public static Vector3 XZ3D(Vector2 a)
        {
            return new(a.x, 0, a.y);
        }

        public static Vector2 RestrictIn(Vector2 p, Vector4 b)
        {
            return new(
                Mathf.Clamp(p.x, b.x - (b.z / 2), b.x + (b.z / 2)),
                Mathf.Clamp(p.y, b.y - (b.w / 2), b.y + (b.w / 2))
            );

        }

        public static Vector3 RestrictIn(Vector3 p, Vector3 min, Vector3 max)
        {
            return new(
                Mathf.Clamp(p.x, min.x, max.x),
                Mathf.Clamp(p.y, min.y, max.y),
                Mathf.Clamp(p.z, min.z, max.z)
            );
        }

        public static Vector3 ProjectClamped(Vector3 beginLine, Vector3 endLine, Vector3 point)
        {
            // get the linear t equivalent, for clamping the projection on the segment
            float t = InverseLerp(beginLine, endLine, Vector3.Project(
                point,
                (endLine - beginLine).normalized
            ));
            t = Mathf.Clamp01(t);

            // return the linear interpolation of t
            return Vector3.Lerp(beginLine, endLine, t);
        }

        public static float GetDistanceBetweenLineAndPoint(Vector3 beginLine, Vector3 endLine, Vector3 point)
        {
            Vector3 closetPoint = ProjectClamped(beginLine, endLine, point);
            return Vector3.Distance(closetPoint, point);
        }

        public static Vector3 ClampToLine(Vector3 beginLine, Vector3 endLine, Vector3 point, float distance = 0.01f)
        {
            float d = GetDistanceBetweenLineAndPoint(beginLine, endLine, point);
            if (d < distance) return point; // nothing to do with the point

            Vector3 projectedPoint = ProjectClamped(beginLine, endLine, point); 
            Vector3 normal = (point - projectedPoint).normalized;

            return projectedPoint + (normal * distance);

        }

        public static Vector3 GetTangentOfLine(Vector3 beginLine, Vector3 endLine)
        {
            Vector3 normal = (endLine - beginLine).normalized;
            return Vector3.Cross(normal, Vector3.up);
        }

    }
}