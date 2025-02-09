using System;
using System.Collections.Generic;
namespace BrickForceDevTools
{
    public class Vector2
    {
        public float x, y;

        public Vector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public static Vector2 RotateVector2(Vector2 vec, float rot)
        {
            float cos = (float)Math.Cos(rot);
            float sin = (float)Math.Sin(rot);
            return new Vector2(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(0f - a.x, 0f - a.y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }
    }
}
