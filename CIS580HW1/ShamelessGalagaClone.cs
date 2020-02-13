using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace CIS580HW
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ShamelessGalagaClone : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ship ship;
        Alien[,] aliens = new Alien[10, 5];
        KeyboardState oldState;
        KeyboardState newState;

        private List<Bullet> Bullets { get; set; }

        public ShamelessGalagaClone()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ship = new Ship(this);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    aliens[i, j] = new Alien(this);
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            ship.Initialize();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    aliens[i, j].Initialize(i, j);
                }
            }

            Bullets = new List<Bullet>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            ship.LoadContent(Content);

            foreach (Alien alien in aliens)
            {
                alien.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            ship.Update(gameTime);

            foreach (Bullet bullet in Bullets)
            {
                if (ship.Bounds.CollidesWith(bullet.Bounds)) // Ship collides with bullet
                {
                    ship.AcceptInput = false;
                    bullet.hitSFX.Play();
                    RemoveProjectile(bullet);
                }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (aliens[i, j].Bounds.CollidesWith(bullet.Bounds)) // Bullet collides with alien
                        {
                            aliens[i, j] = null;
                            bullet.hitSFX.Play();
                            RemoveProjectile(bullet);
                        }
                    }
                }
            }
         
            oldState = newState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            ship.Draw(spriteBatch);

            foreach(Alien alien in aliens)
            {
                alien.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        internal void AddProjectile(IEntity parent)
        {
            Bullet bullet = new Bullet(this);
            bullet.Initialize(parent);

            Bullets.Add(bullet);
        }
        
        internal void RemoveProjectile(Bullet bullet)
        {
            Bullets.Remove(bullet);
        }
    }
}
