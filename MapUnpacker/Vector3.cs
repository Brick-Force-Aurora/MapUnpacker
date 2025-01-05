using System.Numerics;

namespace MapUnpacker
{
    class Vector3
    {
        public float X, Y, Z;

        public Vector3(float _x, float _y, float _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }

        public static Vector3 MultMatri4x4(Matrix4x4 matrix, Vector3 vec)
        {
            return new Vector3(
                vec.X * matrix.M11 + vec.Y * matrix.M12 + vec.Z * matrix.M13 + matrix.M14,
                vec.X * matrix.M21 + vec.Y * matrix.M22 + vec.Z * matrix.M23 + matrix.M24,
                vec.X * matrix.M31 + vec.Y * matrix.M32 + vec.Z * matrix.M33 + matrix.M34
                );
        }

        public static Vector3 RotateVector3(Vector3 vec, float rot)
        {
            Matrix4x4 rotation = Matrix4x4.CreateRotationY(rot);
            return MultMatri4x4(rotation, vec);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(0f - a.X, 0f - a.Y, 0f - a.Z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }
    }
}