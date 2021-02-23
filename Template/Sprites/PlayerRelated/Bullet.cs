using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Sprites.PlayerRelated
{
    class Bullet : Sprite
    {
        Vector2 origin = new Vector2(9, 4);
        public Bullet(Texture2D texture, Vector2 position, Point size, float rotation, Vector2 direction, float speed) : base(texture)
        {
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            this.position = position;
            rectangle = new Rectangle(position.ToPoint(), size);
            this.rotation = rotation;
            this.direction = direction;
            this.speed = speed;
        }
        public void Move()
        {
            position += direction * speed;

            rectangle = new Rectangle(position.ToPoint(), rectangle.Size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), sourceRectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}