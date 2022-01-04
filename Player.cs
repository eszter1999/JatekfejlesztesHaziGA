using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Project1
{
    class Player
    {
        private Matrix localTransform = Matrix.CreateScale(0.5f);
        private Matrix WorldTransform = Matrix.Identity;
        BasicGeometry _shere;

        Vector3 position, aim;
        public Vector3 Position => position;
        public string Name;
        public bool won;

        public Player(string name,Vector3 startPos)
        {
            Name = name;
            position = startPos + Vector3.Up * 3;
            aim = Vector3.Zero;
            won = false;
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
            //Debug.WriteLine(name + " is moving -- " + "Poz: " + position + "   AimFromHere:" + aim + "    MoveTo:" + moveTo);
            position += moveTo;
            aim -= moveTo;
        }

        public void nextMoveTo(Vector3 aim){ this.aim = aim; }
        public void restart(Vector3 start_pos)
        {
            position = start_pos;
            aim = Vector3.Zero;
        }
        public void win()
        {
            this.won = true;
            Debug.WriteLine(Name + " has won.");
            _shere.Effect.DiffuseColor = Color.Blue.ToVector3();
        }

        public void reset(Vector3 start)
        {
            this.won = false;
            _shere.Effect.DiffuseColor = Color.Red.ToVector3();
            position = start+Vector3.Up *3;
            aim = Vector3.Zero;
        }
    }
}
