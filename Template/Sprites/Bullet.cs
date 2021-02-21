using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Sprites
{
    class Bullet : BaseClass
    {
        Vector2 origin = new Vector2(9, 4);
        public Bullet(Texture2D bulletTex, Vector2 bulletPos, Point size, float bulletRot, Vector2 bulletDir, float bulletSpeed)
        {
            sourceRectangle = new Rectangle(0, 0, bulletTex.Width, bulletTex.Height);
            texture = bulletTex;
            position = bulletPos;
            rectangle = new Rectangle(bulletPos.ToPoint(), size);
            rotation = bulletRot;
            direction = bulletDir;
            speed = bulletSpeed;
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