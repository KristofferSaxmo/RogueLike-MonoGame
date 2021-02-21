using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Animation
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int FrameTime { get; set; }
        public int Scale { get; set; }
        public Point Size { get; set; }

        private int currentFrame;
        private int totalFrames;
        private int frameCountdown;

        public Animation(Texture2D texture, Vector2 position, int frameTime, int rows, int columns, int scale)
        {
            Texture = texture;
            Position = position;
            FrameTime = frameTime;
            Rows = rows;
            Columns = columns;
            Scale = scale;
            Size = new Point(Texture.Width * Scale * rows, Texture.Height * Scale * columns);
            frameCountdown = FrameTime;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update()
        {
            if (frameCountdown == 0)
            {
                currentFrame++;
                frameCountdown = FrameTime;
            }

            frameCountdown--;

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle(position.ToPoint(), Size);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
