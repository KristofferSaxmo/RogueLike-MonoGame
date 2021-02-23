using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Sprites.PlayerRelated
{
    class Player_Shadow : Sprite
    {
        public Player_Shadow(Texture2D texture, int scale) : base(texture)
        {
            this.scale = scale;
            size = new Point(texture.Width * scale, texture.Height * scale);
        }

        public void Update(Vector2 playerPos)
        {
            position = new Vector2(playerPos.X + 4 * scale, playerPos.Y + 14 * scale);
            rectangle = new Rectangle(position.ToPoint(), size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }
}
