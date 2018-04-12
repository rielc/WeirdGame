using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoGame37Cross
{
    public class Player
    {
        public SpriteBatch spriteBatch;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 jumpDirection;
        public Viewport Viewport;
        public int impulse = 60;
        public bool isJumping;
        public bool isFalling;
        public bool isTouching;
        public int jumpedAtTimestamp;
        public int jumpFinishTimestamp;
        public int jumpDurationInMS = 130;
        public int lastUpdateTimestamp;


        public Player()
        {
            isJumping = false;
            isFalling = false;
            isTouching = false;
        }

        public void Initialize(SpriteBatch _spriteBatch, Texture2D _texture) {
            spriteBatch = _spriteBatch;
            texture = _texture;
        }

        public void SetPosition(Vector2 _position) {
            position = new Vector2(_position.X, _position.Y);
        }


        public void Jump(Vector2 _direction)
        {
            if (!isJumping) {
                jumpDirection = Vector2.Normalize(
                    new Vector2(_direction.X * -1.0f, _direction.Y)
                );
                jumpedAtTimestamp = lastUpdateTimestamp;
                jumpFinishTimestamp = jumpedAtTimestamp + jumpDurationInMS;
                isJumping = true;
            }
        }


        public void SetViewport(Viewport _viewport) {
            Viewport = _viewport;
        }


        public void Update(GameTime _gameTime)
        {
            lastUpdateTimestamp = (int)_gameTime.TotalGameTime.TotalMilliseconds;


            if (isJumping) {
                var impulseStrength = (float) (jumpFinishTimestamp - lastUpdateTimestamp) / (float) jumpDurationInMS;
                if (jumpFinishTimestamp < lastUpdateTimestamp)
                {
                    isJumping = false;
                    impulseStrength = 0.0f;
                }

                var jumpVelocity = Vector2.Lerp(
                    jumpDirection,
                    new Vector2(0.0f, 0.0f),
                    MathHelper.SmoothStep(impulse, 0, impulseStrength)
                );

                Console.Write("jumpProgress: " + impulseStrength + "\n");
                Console.Write("jumpDurationInMS: " + jumpDurationInMS + "\n");
                Console.Write("jumpFinishTimestamp: " + jumpFinishTimestamp + "\n");
                Console.Write("lastUpdateTimestamp: " + lastUpdateTimestamp + "\n");
                Console.Write("------------------------ ");

                position = Vector2.Add(position, jumpVelocity);
            }
            var Gravity = new Vector2(0, 9.8f);
            position = Vector2.Add(position, Gravity);

            var touchRight = (position.X + texture.Width / 2) >= Viewport.Width;
            var touchLeft = (position.X - texture.Width / 2) <= 0;
            var touchTop = (position.Y - texture.Height / 2) <= 0;
            var touchBottom = (position.Y + texture.Height / 2) >= Viewport.Height;

            if (touchRight) position.X = Viewport.Width - (texture.Width/ 2);
            if (touchLeft) position.X = texture.Width / 2;
            if (touchTop) position.Y = texture.Height / 2;
            if (touchBottom) position.Y = Viewport.Height - (texture.Height / 2);
        }



        public void Draw(GameTime gameTime)
        {
            var spritePosition = new Vector2(position.X - texture.Width / 2, position.Y - texture.Height / 2);
            spriteBatch.Draw(texture, spritePosition);
        }

    }
}
