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
        Texture2D playerSprite;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 ballPosition;
        Player player;
        AnimatedSprite playerAnimation;
        float ballSpeed;
        int lastBulletIndex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player();
        }

        protected override void Initialize()
        {
            ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 200f;
            bullets = new List<Projectile>();
            lastBulletIndex = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureBall = Content.Load<Texture2D>("player");
            textureBullet = Content.Load<Texture2D>("bullet");
            playerSprite = Content.Load<Texture2D>("char_side_highres");
            playerAnimation = new AnimatedSprite(playerSprite, 1, 10, 36, 48, 10);

            player.Initialize(spriteBatch, textureBall);
            player.SetViewport(graphics.GraphicsDevice.Viewport);
            player.SetPosition(
                new Vector2(
                    graphics.GraphicsDevice.Viewport.Width / 2,
                    graphics.GraphicsDevice.Viewport.Height / 2
                )
            );
            AseSpriteAnimation test = new AseSpriteAnimation("mustache");
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


            if (isDown || isUp || isRight || isLeft) {
                playerAnimation.play();
            } else {
                playerAnimation.stop();
            }

            playerAnimation.Update(gameTime);

            var isShooting = true; // gState.Buttons.A == ButtonState.Pressed;
            var isJumping = gState.Buttons.B == ButtonState.Pressed;

            player.Update(gameTime);
            if (isJumping) player.Jump(gState.ThumbSticks.Left);

            if (isShooting) {
                var bullet = new Projectile();
                bullet.Initialize(ballPosition, bullets.Count);
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
            //GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            playerAnimation.Draw(spriteBatch, ballPosition, 0);

            foreach (var bullet in bullets)
            {
                playerAnimation.Draw(spriteBatch, bullet.position, bullet.index);
            }

            playerAnimation.Draw(spriteBatch, player.position, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
