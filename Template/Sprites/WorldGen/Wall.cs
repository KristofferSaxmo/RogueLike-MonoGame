using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Sprites.WorldGen
{
    class Wall : Sprite
    {
        public Wall(Texture2D texture, Vector2 position, Point size) : base(texture)
        {
            this.position = position;
            rectangle = new Rectangle(position.ToPoint(), size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.MintCream);
        }
    }
}
