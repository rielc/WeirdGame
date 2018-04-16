using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame37Cross
{
    public class AnimatedSprite
    {
        public Texture2D texture { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        public int framerate = 10;
        private int lastFrameTimestamp = 0;
        private bool isAnimated = true;
        public int width = 0;
        public int height = 0;

        public AnimatedSprite(Texture2D _texture, int _rows, int _columns, int _width, int _height, int _totalFrames)
        {
            texture = _texture;
            rows = _rows;
            columns = _columns;
            currentFrame = 0;
            totalFrames = _totalFrames;
            width = _width;
            height = _height;
        }

        public void play() {
            isAnimated = true;
        }

        public void stop()
        {
            isAnimated = false;
        }

        public void setFramerate(int _framerame) {
            framerate = _framerame;
        }

        public void Update(GameTime _gameTime)
        {
            if (isAnimated && (lastFrameTimestamp + (int) (1000 / framerate)) <= _gameTime.TotalGameTime.TotalMilliseconds) {
                currentFrame++;
                lastFrameTimestamp = (int) _gameTime.TotalGameTime.TotalMilliseconds;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int _rotation)
        {
            int row = (int)((float)currentFrame / (float) columns);
            int column = (currentFrame+_rotation) % columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width*2, height*2);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
