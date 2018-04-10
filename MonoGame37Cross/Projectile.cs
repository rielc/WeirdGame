using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame37Cross

{
    public class Projectile
    {
        public float speed = 1;
        public Vector2 position;
        public SpriteBatch spriteBatch;
        public Texture2D texture;

        public Projectile()
        {
            
        }

        public void Initialize(Vector2 p)
        {
            position = new Vector2(p.X, p.Y);
        }


        public void Update(GameTime gameTime)
        {
            if (speed < 50.0f)
            {
                speed *= 1.1f;
            }
            position.X += speed;
        }

        public void Draw(SpriteBatch s, Texture2D t)
        {
            s.Draw(t, position);
        }
    }


}
