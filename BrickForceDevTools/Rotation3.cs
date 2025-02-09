using System;
using System.Collections.Generic;
using System.Linq;
namespace BrickForceDevTools
{
    class Rotation3
    {
        public float P, Y, R;

        public Rotation3(float _x, float _y, float _z)
        {
            P = _x;
            Y = _y;
            R = _z;
        }

        public static Rotation3 operator +(Rotation3 a, Rotation3 b)
        {
            return new Rotation3(a.P + b.P, a.Y + b.Y, a.R + b.R);
        }

        public static Rotation3 operator -(Rotation3 a, Rotation3 b)
        {
            return new Rotation3(a.P - b.P, a.Y - b.Y, a.R - b.R);
        }

        public static Rotation3 operator -(Rotation3 a)
        {
            return new Rotation3(0f - a.P, 0f - a.Y, 0f - a.R);
        }

        public static Rotation3 operator *(Rotation3 a, float d)
        {
            return new Rotation3(a.P * d, a.Y * d, a.R * d);
        }

        public static Rotation3 operator *(float d, Rotation3 a)
        {
            return new Rotation3(a.P * d, a.Y * d, a.R * d);
        }
    }
}
