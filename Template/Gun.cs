using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Template
{
    class Gun : BaseClass
    {
        float leftRotation;
        float rightRotation;

        float bulletRot;
        Vector2 bulletDir;

        int shootCooldown;

        public Gun(Texture2D gunTex, int gunDamage, int gunRateOfFire)
        {
            sourceRectangle = new Rectangle(0, 0, gunTex.Width, gunTex.Height);
            leftOrigin = new Vector2(gunTex.Width, gunTex.Height / 2);
            rightOrigin = new Vector2(0, gunTex.Height / 2);
            damage = gunDamage;
            rateOfFire = gunRateOfFire;
        }
        public void Shoot(List<Bullet> bullets, Texture2D bulletTex, int gunOffset)
        {
            var mouseState = Mouse.GetState();
            if (shootCooldown == 0 && mouseState.LeftButton == ButtonState.Pressed)
            {
                bulletDir.X = mouseState.X - position.X - gunOffset;
                bulletDir.Y = mouseState.Y - position.Y;
                bulletDir.Normalize();

                bulletRot = (float)Math.Atan2(bulletDir.Y, bulletDir.X);

                bullets.Add(new Bullet(
                    bulletTex, // Texture
                    new Vector2(position.X + gunOffset, position.Y), // Position
                    new Point(2, 2), // Size
                    (float)Math.Atan2(bulletDir.Y, bulletDir.X), // Rotation
                    new Vector2((float)Math.Cos(bulletRot), (float)Math.Sin(bulletRot)), // Direction
                    15)); // Speed

                shootCooldown = rateOfFire;
            }
            else if (shootCooldown > 0)
                shootCooldown--;
        }
        public void Update(Vector2 playerPos, Texture2D gunTex)
        {
            position = playerPos;
            texture = gunTex;
            var mouseState = Mouse.GetState();

            direction.X = mouseState.X - position.X;
            direction.Y = mouseState.Y - position.Y;
            direction.Normalize();

            leftRotation = (float)Math.Atan2(0 - direction.Y, 0 - direction.X);   // Left angle
            rightRotation = (float)Math.Atan2(direction.Y, direction.X);          // Right angle

        }
        public void DrawRight(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 20, position.Y + 12), sourceRectangle, Color.White, rightRotation, rightOrigin, 1.0f, SpriteEffects.None, 1);
        }
        public void DrawLeft(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 10, position.Y + 12), sourceRectangle, Color.White, leftRotation, leftOrigin, 1.0f, SpriteEffects.None, 1);
        }
    }
}