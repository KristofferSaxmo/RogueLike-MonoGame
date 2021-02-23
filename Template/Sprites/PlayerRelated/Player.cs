using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Template.Sprites.WorldGen;

namespace Template.Sprites
{
    class Player : Sprite
    {
        private Animation walkLeftAnimation, walkRightAnimation;
        private bool isFacingLeft, walking;
        public Player(Texture2D texture, Texture2D walkLeftTex, Texture2D walkRightTex, Vector2 position, int frameTime, int rows, int cols, int scale) : base(texture)
        {
            walkLeftAnimation = new Animation(walkLeftTex, frameTime, rows, cols, scale);
            walkRightAnimation = new Animation(walkRightTex, frameTime, rows, cols, scale);
            this.position = position;
            this.scale = scale;
            rectangle = new Rectangle(position.ToPoint(), new Point(texture.Width * scale, texture.Height * scale));
            hitbox = new Rectangle((int)position.X, (int)position.Y + 21 * scale, 11 * scale, 7 * scale);
        }
        public void MovePlayer()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A)) // Left
                velocity.X = -5;

            if (keyboardState.IsKeyDown(Keys.D)) // Right
                velocity.X = 5;

            if (keyboardState.IsKeyDown(Keys.W)) // Up
                velocity.Y = -5;

            if (keyboardState.IsKeyDown(Keys.S)) // Down
                velocity.Y = 5;
        }
        public void UpdateHitbox()
        {
            rectangle = new Rectangle(position.ToPoint(), rectangle.Size); // Rectangle = Position
            hitbox = new Rectangle((int)position.X, (int)position.Y + 21 * scale, 11 * scale, 7 * scale);
        }

        public void Update(List<Wall> walls, bool isFacingLeft)
        {
            MovePlayer();

            foreach (var wall in walls)
            {
                if ((velocity.X > 0 && IsTouchingLeft(wall)) ||
                (velocity.X < 0 & IsTouchingRight(wall)))
                    velocity.X = 0;

                if ((velocity.Y > 0 && IsTouchingTop(wall)) ||
                    (velocity.Y < 0 & IsTouchingBottom(wall)))
                    velocity.Y = 0;
            }

            this.isFacingLeft = isFacingLeft;

            if (velocity.X != 0 || velocity.Y != 0)
                walking = true;
            else
                walking = false;

            if (walking)
            {
                if (isFacingLeft)
                    walkLeftAnimation.Update();
                else
                    walkRightAnimation.Update();
            }

            position += velocity;

            velocity = Vector2.Zero;

            UpdateHitbox();
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {

            if (walking)
            {
                if(isFacingLeft)
                    walkLeftAnimation.Draw(spriteBatch, position);
                else
                    walkRightAnimation.Draw(spriteBatch, position);
                return;
            }
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        protected bool IsTouchingLeft(Wall wall)
        {
            return hitbox.Right + velocity.X > wall.Rectangle.Left &&
              hitbox.Left < wall.Rectangle.Left &&
              hitbox.Bottom > wall.Rectangle.Top &&
              hitbox.Top < wall.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Wall wall)
        {
            return hitbox.Left + velocity.X < wall.Rectangle.Right &&
              hitbox.Right > wall.Rectangle.Right &&
              hitbox.Bottom > wall.Rectangle.Top &&
              hitbox.Top < wall.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Wall wall)
        {
            return hitbox.Bottom + velocity.Y > wall.Rectangle.Top &&
              hitbox.Top < wall.Rectangle.Top &&
              hitbox.Right > wall.Rectangle.Left &&
              hitbox.Left < wall.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Wall wall)
        {
            return hitbox.Top + velocity.Y < wall.Rectangle.Bottom &&
              hitbox.Bottom > wall.Rectangle.Bottom &&
              hitbox.Right > wall.Rectangle.Left &&
              hitbox.Left < wall.Rectangle.Right;
        }
    }
}