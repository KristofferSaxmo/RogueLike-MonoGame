using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Sprites.WorldGen
{
    class Wall : Sprite
    {
        public Wall(Texture2D wallTex, Vector2 wallPos, Point size)
        {
            texture = wallTex;
            position = wallPos;
            rectangle = new Rectangle(wallPos.ToPoint(), size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.MintCream);
        }
    }
}
