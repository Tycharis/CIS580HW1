using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CIS580HW
{
    class Bullet
    {
        /// <summary>
        /// The game object
        /// </summary>
        ShamelessGalagaClone game;

        /// <summary>
        /// This bullet's bounds
        /// </summary>
        public BoundingRectangle Bounds;

        /// <summary>
        /// This bullet's texture
        /// </summary>
        Texture2D texture;

        float DirectionalMultiplier;

        public SoundEffect fireSFX;
        public SoundEffect hitSFX;

        /// <summary>
        /// Creates a bullet
        /// </summary>
        /// <param name="game">The game this bullet belongs to</param>
        public Bullet(ShamelessGalagaClone game)
        {
            this.game = game;
        }

        /// <summary>
        /// Initializes the bullet, setting its initial size 
        /// and centering it on the firing entity.
        /// </summary>
        public void Initialize(IEntity parent)
        {
            DirectionalMultiplier = parent is Alien ? 1 : -1;

            Bounds = new BoundingRectangle(
                parent.Bounds.X + parent.Bounds.Width / 2,                      // X
                parent.Bounds.Y + (parent is Alien ? parent.Bounds.Height + 30 : -30), // Y
                2,                                                              // Width
                8                                                               // Height
            );
        }

        /// <summary>
        /// Loads the bullet's content
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bullet");
            fireSFX = content.Load<SoundEffect>("fire");
            hitSFX = DirectionalMultiplier == 1 ? content.Load<SoundEffect>("explode") : content.Load<SoundEffect>("hit");
        }

        /// <summary>
        /// Updates the bullet
        /// </summary>
        /// <param name="gameTime">The game's GameTime</param>
        public void Update(GameTime gameTime)
        {   
            // move up or down
            Bounds.Y += (float) gameTime.ElapsedGameTime.TotalMilliseconds * DirectionalMultiplier;

            // Unload the round when going off-screen
            if (Bounds.Y < 0)
            {
                game.RemoveProjectile(this);
            }

            if (Bounds.Y > game.GraphicsDevice.Viewport.Height - Bounds.Height)
            {
                game.RemoveProjectile(this);
            }
        }

        /// <summary>
        /// Draw the bullet
        /// </summary>
        /// <param name="spriteBatch">
        /// The SpriteBatch to draw the bullet with.  This method should 
        /// be invoked between SpriteBatch.Begin() and SpriteBatch.End() calls.
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
