using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//using TiledSharp;


namespace MonoGame37Cross.Desktop
{
    public class Game1 : Game
    {
        List<Projectile> bullets;
        Texture2D textureBall;
        Texture2D textureBullet;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;		
        Vector2 ballPosition;
        float ballSpeed;
        int lastBulletIndex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 300f;
            bullets = new List<Projectile>();
            lastBulletIndex = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureBall = Content.Load<Texture2D>("player");
            textureBullet = Content.Load<Texture2D>("bullet");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            var gState = GamePad.GetState(PlayerIndex.One);

            var leftStickDirection = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;

            var isDown = kstate.IsKeyDown(Keys.Down) || gState.DPad.Down == ButtonState.Pressed;
            var isUp = kstate.IsKeyDown(Keys.Up) || gState.DPad.Up == ButtonState.Pressed;
            var isRight= kstate.IsKeyDown(Keys.Right) || gState.DPad.Right == ButtonState.Pressed;
            var isLeft = kstate.IsKeyDown(Keys.Left) || gState.DPad.Left == ButtonState.Pressed;

            var isShooting= gState.Buttons.A == ButtonState.Pressed;


            if (isShooting) {
                var bullet = new Projectile();
                bullet.Initialize(ballPosition);
                bullets.Add(bullet);
            }

            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
            }

            //for (int i = 0; i <= lastBulletIndex; i++)
            //{
            //    bullets[i].Update(gameTime);
            //}

            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isUp) ballPosition.Y -= ballSpeed * elapsedTime;
            if (isDown) ballPosition.Y += ballSpeed * elapsedTime;
            if (isLeft) ballPosition.X -= ballSpeed * elapsedTime;
            if (isRight) ballPosition.X += ballSpeed * elapsedTime;


            if (
                GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                kstate.IsKeyDown(Keys.Escape)
            ) {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();
            spriteBatch.Draw(textureBall, ballPosition, Color.White);
            //for (int i = 0; i < bullets.Length; i++)
            //{
            //    bullets[i].Draw(spriteBatch, textureBullet);
            //}

            foreach (var bullet in bullets)
            {
                bullet.Draw(spriteBatch, textureBullet);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
