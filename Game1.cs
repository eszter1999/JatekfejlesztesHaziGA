using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1
{

    enum Directions { STAY, RIGHT, LEFT, UP  };
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Track track;
        private Player player1;
        private KeyboardState previousKeys;
        private List<Vector3> track_heights = new List<Vector3> {
            new Vector3(-10,0,0),
            new Vector3(-7,0,0),
            new Vector3(-4,0,0),
            new Vector3(-1,0,0),
            new Vector3(2,0,0),
            new Vector3(2,3,0),
            new Vector3(5,0,0),
            new Vector3(8,0,0),
            new Vector3(11,0,0),
            new Vector3(14,0,0)
            };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.Window.ClientSizeChanged += (s, e) =>
            { 
                this._graphics.PreferredBackBufferWidth = this.Window.ClientBounds.Width;
                this._graphics.PreferredBackBufferHeight = this.Window.ClientBounds.Height;
                _graphics.ApplyChanges();
            };
        }

        protected override void Initialize()
        {
            
            track = new Track(track_heights);
            player1 = new Player("player1",track.getStartPos());
            previousKeys = Keyboard.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            track.load(GraphicsDevice);
            player1.load(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();
            if (player1.isThisMoving())
            {
                player1.move();
                Debug.WriteLine(track.getGoalPos() + Vector3.Up * 3);
                if (player1.Position == (track.getGoalPos() + Vector3.Up * 3))
                {
                    player1.Position = track.getStartPos();
                    player1.win();
                }

            }
            else
            {
                Vector3 fallMove = track.fallTo(player1.Position);
                if (fallMove == Vector3.Up)
                    player1.restart(track.getStartPos());
                if (state.IsKeyDown(Keys.Right) && previousKeys.IsKeyUp(Keys.Right))
                    movePlayer(player1, Directions.RIGHT);
                if (state.IsKeyDown(Keys.Left) && previousKeys.IsKeyUp(Keys.Left))
                    movePlayer(player1, Directions.LEFT);
                if (state.IsKeyDown(Keys.Up) && previousKeys.IsKeyUp(Keys.Up))
                    movePlayer(player1, Directions.UP);
                if (fallMove != Vector3.Zero)
                    player1.nextMoveTo(fallMove);
            }
            previousKeys = state;
            base.Update(gameTime);
        }
        void movePlayer(Player p, Directions d)
        {
            switch (d)
            {
                case Directions.RIGHT:
                    Debug.WriteLine("Move to the right");
                    if (!track.isBlocked(p.Position + Vector3.Right*3))
                        p.nextMoveTo(Vector3.Right * 3);
                    break;
                case Directions.LEFT:
                    if (!track.isBlocked(p.Position + Vector3.Left*3))
                        p.nextMoveTo(Vector3.Left * 3);
                    break;
                case Directions.UP:
                    if(!track.isBlocked(p.Position + Vector3.Up*3 + Vector3.Right*3))
                        p.nextMoveTo(Vector3.Up * 3 + Vector3.Right * 3);
                    break;
                default:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Camera.Main.AspectRatio = GraphicsDevice.Viewport.AspectRatio;
            track.draw(Camera.Main);
            player1.draw(Camera.Main);

            base.Draw(gameTime);
        }
    }
}
