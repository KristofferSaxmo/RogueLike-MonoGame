using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Wall : BaseClass
    {
        public Wall(Texture2D wallTex, Vector2 wallPos, Point size)
        {
            texture = wallTex;
            position = wallPos;
            rectangle = new Rectangle(wallPos.ToPoint(), size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
