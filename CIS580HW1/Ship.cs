using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CIS580HW
{
    class Ship : IEntity
    {
        private const int MARGIN = 32;
        private const int SPRITE_WIDTH = 32;
        private const int SPRITE_HEIGHT = 32;
        private const int FIRE_DELAY_MS = 500;

        ShamelessGalagaClone Game;

        public BoundingRectangle Bounds { get; set; }

        Texture2D Texture;

        public bool AcceptInput;

        float LastFire;

        public Ship(ShamelessGalagaClone game)
        {
            Game = game;
        }

        public void Initialize()
        {
            Bounds = new BoundingRectangle(Game.GraphicsDevice.Viewport.Width / 2 - SPRITE_WIDTH / 2,
                Game.GraphicsDevice.Viewport.Height - SPRITE_HEIGHT - MARGIN, 32, 32);

            AcceptInput = true;
        }

        public void LoadContent(ContentManager manager)
        {
            Texture = manager.Load<Texture2D>("ship");
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            // Move the ship left if the left key is pressed
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // move left
                Bounds.X -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Move the ship right if the right key is pressed
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                // move right
                Bounds.X += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // Fire the cannon if space is pressed and delay is no factor
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                float currentTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (currentTime < LastFire + FIRE_DELAY_MS)
                {
                    Game.AddProjectile(this);
                    LastFire = currentTime;
                }
            }

            // Stop the ship from going off-screen
            if (Bounds.X < 32)
            {
                Bounds.X = 32;
            }

            if (Bounds.X > Game.GraphicsDevice.Viewport.Width - Bounds.Width - MARGIN)
            {
                Bounds.X = Game.GraphicsDevice.Viewport.Width - Bounds.Width - MARGIN;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }
}
