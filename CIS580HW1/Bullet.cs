using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CIS580HW1
{
    class Bullet
    {
        /// <summary>
        /// The game object
        /// </summary>
        ShamelessGalagaClone game;

        /// <summary>
        /// This paddle's bounds
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// This paddle's texture
        /// </summary>
        Texture2D texture;

        float DirectionalMultiplier;

        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">The game this paddle belongs to</param>
        public Bullet(ShamelessGalagaClone game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the paddle, setting its initial size 
        /// and centering it on the firing entity.
        /// </summary>
        public void Initialize(IEntity parent)
        {
            DirectionalMultiplier = parent is Alien ? 1 : -1;

            Bounds.Width = 2;
            Bounds.Height = 8;
            Bounds.X = parent.Bounds.X + parent.Bounds.Width / 2;
            Bounds.Y = parent.Bounds.Y + parent.Bounds.Height * DirectionalMultiplier;
        }

        /// <summary>
        /// Loads the paddle's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pixel");
        }

        /// <summary>
        /// Updates the paddle
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {   
            // move up or down
            Bounds.Y += (float) gameTime.ElapsedGameTime.TotalMilliseconds * DirectionalMultiplier;

            // Unload the round when going off-screen
            if (Bounds.Y < 0)
            {
                //TODO unload
            }

            if (Bounds.Y > game.GraphicsDevice.Viewport.Height - Bounds.Height)
            {
                //TODO unload
            }
        }

        /// <summary>
        /// Draw the paddle
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the paddle with.  This method should 
        /// be invoked between SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.Green);
        }
    }
}
