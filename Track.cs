using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Project1
{
    class Track
    {
        private Matrix localTransform = Matrix.CreateScale(0.5f);
        private Matrix WorldTransform = Matrix.Identity;

        private const int _from = 14, _till = -10;

        private List<BasicGeometry> path = new List<BasicGeometry>();
        private List<Vector3> poz = new List<Vector3>();

        public Track(List<Vector3> list)
        {
            poz = list;
        }

        public Vector3 getStartPos() { return poz[0]; }
        public Vector3 getGoalPos() { return poz[poz.Count - 1]; }

        public void load(GraphicsDevice graphicsDevice)
        {
            foreach (var item in poz)
            {
                BasicGeometry temp = BasicGeometry.CreateCube(graphicsDevice);
                temp.Effect.EnableDefaultLighting();
                temp.Effect.DiffuseColor = Color.White.ToVector3();
                path.Add(temp);
            }
            path[0].Effect.DiffuseColor = Color.Beige.ToVector3();
            path[poz.Count-1].Effect.DiffuseColor = Color.Green.ToVector3(); 
        }

        public void draw(Camera cam)
        {
            var pozEnumerator = poz.GetEnumerator();
            var pathEnumerator = path.GetEnumerator();
            while (pozEnumerator.MoveNext() && pathEnumerator.MoveNext())
            {
                Matrix local = localTransform + Matrix.CreateTranslation(pozEnumerator.Current);
                pathEnumerator.Current.Draw(local * WorldTransform, cam.View, cam.Projection);
            }
        }

        public Vector3 fallTo(Vector3 player_pos)
        {
            float height = 1000;
            for (var i = 0; i < path.Count; i++)
            {
                float distance = samePlace(player_pos, poz[i]);
                if (distance > 0 && height>= distance)
                    height = distance;
            }
            if (height != 1000)
                return Vector3.Down * (height-3);
            return Vector3.Up;
        }
        
        //ha a kettő egy vonalban van, akkor visszaadja a p magasságát a track-tól mérve
        //hanem 0-t ad
        //ha negatív akkor alatta kell legyen - ami nem jó a mi esetünkben
        private float samePlace(Vector3 p, Vector3 track)
        {
            if (p.X == track.X && p.Z == track.Z)
                return p.Y - track.Y;
            return 0;
        }

        public bool isBlocked (Vector3 destination)
        {
            foreach (var item in poz)
            {
                if (destination == item)
                    return true;
            }
            return false;
        }
    }
}
