using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    class Camera
    {
        public Vector3 Position = new Vector3(2, 5, 12);
        public Vector3 Direction = new Vector3(-1, -3, -8);
        public Vector3 Up = Vector3.Up;
        public float AspectRatio = 1;
        public Matrix View => Matrix.CreateLookAt(Position, Position + Direction, Up);
        public Matrix Projection => Matrix.CreatePerspectiveFieldOfView(1, AspectRatio, 1, 1010);
        public static readonly Camera Main = new Camera();
    }
}
