using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoGame37Cross
{
    public class Player
    {

        public Vector2 Position;
        public Vector2 Velocity;
        public int Impulse = 30;
        public SpriteBatch SpriteBatch;
        public Texture2D Texture;
        public bool IsJumping;
        public bool IsFalling;
        public bool IsTouching;


        public Player(SpriteBatch _spriteBatch, Texture2D _texture)
        {
            SpriteBatch = _spriteBatch;
            Texture = _texture;
            IsJumping = false;
            IsFalling = false;
            IsTouching = false;
            Velocity = new Vector2(0, 0);
        }

        public void jump(Vector2 _direction)
        {
            Velocity = _direction * Impulse;
        }

        public void Initialize(Vector2 _initialPosition) {
            Position = new Vector2(_initialPosition.X, _initialPosition.Y);

        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
            Velocity.X *= 0.8;
            Velocity.Y *= 0.8;
        }

        public void Update(GameTime gameTime)
        {
            if (speed < 50.0f)
            {
                speed *= 1.1f;
            }
            position.X += speed;
        }

    }
}
