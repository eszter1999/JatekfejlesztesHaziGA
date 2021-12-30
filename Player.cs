using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Project1
{
    class Player
    {
        private Matrix localTransform = Matrix.CreateScale(0.5f);
        private Matrix WorldTransform = Matrix.Identity;

        Vector3 position, aim;
        BasicGeometry _shere;
        public Vector3 Position => position;
        string name;
        bool won;

        public Player(string s,Vector3 startPos)
        {
            name = s;
            position = startPos + Vector3.Up * 3;
            aim = Vector3.Zero;
            won = false;
        }
        public void restart(Vector3 start_pos) {
            Debug.WriteLine(name + " has to restart.");
            position = start_pos;
            aim = Vector3.Zero;
        }
        public void win() { this.won = true;
            Debug.WriteLine(name + " has won.");
            _shere.Effect.DiffuseColor = Color.Blue.ToVector3();
        }

        public void load(GraphicsDevice graphicsDevice)
        {
            _shere = BasicGeometry.CreateSphere(graphicsDevice);
            _shere.Effect.EnableDefaultLighting();
            if (!won)
                _shere.Effect.DiffuseColor = Color.Red.ToVector3();
            else
                _shere.Effect.DiffuseColor = Color.Blue.ToVector3();
        }

        public void draw(Camera cam)
        {
            Matrix local1 = localTransform + Matrix.CreateTranslation(position);
            _shere.Draw(local1 * WorldTransform, cam.View, cam.Projection);
        }

        public bool isThisMoving() { return (aim != Vector3.Zero); }

        public void move()
        {
            Vector3 moveTo;
            if ((aim.Length() % 1) != 0)
                moveTo = new Vector3(1, 1, 0);
            else
                moveTo = Vector3.Normalize(aim);
            position += moveTo;
            aim -= moveTo;
            Debug.WriteLine(name + " is moving -- " + "Poz: " + position + "   AimFromHere:" + aim + "    MoveTo:" + moveTo);
            if (aim == Vector3.Zero) Debug.WriteLine("Movement ends.");
        }

        public void nextMoveTo(Vector3 aim){
            this.aim = aim;
        }
    }
}
