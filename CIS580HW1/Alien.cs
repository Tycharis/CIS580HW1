using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CIS580HW
{
    class Alien : IEntity
    {
        private const int MARGIN = 32;
        private const int SPRITE_WIDTH = 32;
        private const int SPRITE_HEIGHT = 32;
        private const int TOPLEFT_X = 50;
        private const int TOPLEFT_Y = 50;
        private const int PADDING = 20;

        ShamelessGalagaClone Game;

        public BoundingRectangle Bounds { get; set; }

        Texture2D Texture;

        public Alien(ShamelessGalagaClone game)
        {
            Game = game;
        }

        public void Initialize(int x, int y)
        {
            Bounds = new BoundingRectangle(
                (SPRITE_WIDTH + PADDING) * x + TOPLEFT_X,
                (SPRITE_HEIGHT + PADDING) * y + TOPLEFT_Y,
                SPRITE_WIDTH,
                SPRITE_HEIGHT
            );
        }

        public void LoadContent(ContentManager manager)
        {
            Texture = manager.Load<Texture2D>("alien");
        }

        public void Update(GameTime gameTime)
        {
            // TODO do movement

            // Stop the ship from going off-screen
            if (Bounds.X < MARGIN)
            {
                Bounds.X = MARGIN;
            }

            if (Bounds.X > Game.GraphicsDevice.Viewport.Height - Bounds.Height - MARGIN)
            {
                Bounds.X = Game.GraphicsDevice.Viewport.Height - Bounds.Height - MARGIN;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, Color.White);
        }
    }
}
