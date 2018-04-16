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
        public int index;

        public Projectile()
        {
            
        }

        public void Initialize(Vector2 p, int i)
        {
            index = i;
            position = new Vector2(p.X, p.Y);
        }


        public void Update(GameTime gameTime)
        {

            speed += 1.1f * (float) Math.Sin((float)gameTime.ElapsedGameTime.Milliseconds / (10 + index));
            position.X += (float) Math.Sin(speed);
            position.Y += (float) Math.Sin((float) gameTime.TotalGameTime.TotalMilliseconds / (float) 1000) * 0.2f;
        }

        public void Draw(SpriteBatch s, Texture2D t)
        {
            s.Draw(t, position);
        }
    }


}
