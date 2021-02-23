using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Template.Sprites.PlayerRelated
{
    class Gun : Sprite
    {
        float leftRotation;
        float rightRotation;

        float bulletRot;
        Vector2 bulletDir, targetWorldPosition;

        int shootCooldown;

        public Gun(Texture2D texture, int damage, int rateOfFire) : base(texture)
        {
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            leftOrigin = new Vector2(texture.Width, texture.Height / 2);
            rightOrigin = new Vector2(0, texture.Height / 2);
            this.damage = damage;
            this.rateOfFire = rateOfFire;
        }
        public void Shoot(List<Bullet> bullets, Texture2D bulletTex, int gunOffset)
        {
            var mouseState = Mouse.GetState();
            if (shootCooldown == 0 && mouseState.LeftButton == ButtonState.Pressed)
            {
                bulletDir.X = targetWorldPosition.X - position.X - gunOffset;
                bulletDir.Y = targetWorldPosition.Y - position.Y;
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
        public void Update(Camera camera, Vector2 playerPos, Texture2D gunTex)
        {
            targetWorldPosition = camera.GetWorldPosition(
            new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            position = playerPos;
            texture = gunTex;

            direction.X = targetWorldPosition.X - position.X;
            direction.Y = targetWorldPosition.Y - position.Y;
            direction.Normalize();

            leftRotation = (float)Math.Atan2(0 - direction.Y, 0 - direction.X);   // Left angle
            rightRotation = (float)Math.Atan2(direction.Y, direction.X);          // Right angle

        }
        public void DrawRight(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 20, position.Y + 12), sourceRectangle, Color.White, rightRotation, rightOrigin, 1.0f, SpriteEffects.None, 1);
        }
        public void DrawLeft(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 10, position.Y + 12), sourceRectangle, Color.White, leftRotation, leftOrigin, 1.0f, SpriteEffects.None, 1);
        }
    }
}