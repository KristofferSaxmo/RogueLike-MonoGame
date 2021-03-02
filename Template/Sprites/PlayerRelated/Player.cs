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
        private bool walking, isFacingLeft;
        private KeyboardState keyboardState;
        public Player(Texture2D texture, Texture2D walkLeftTex, Texture2D walkRightTex, Vector2 position, int frameTime, int rows, int cols) : base(texture)
        {
            walkLeftAnimation = new Animation(walkLeftTex, frameTime, rows, cols, scale);
            walkRightAnimation = new Animation(walkRightTex, frameTime, rows, cols, scale);
            this.position = position;
            rectangle = new Rectangle(position.ToPoint(), new Point(texture.Width * scale, texture.Height * scale));
            hitbox = new Rectangle((int)position.X, (int)position.Y + 21 * scale, 11 * scale, 7 * scale);
        }
        public void MovePlayer()
        {
            if (keyboardState.IsKeyDown(Keys.A)) // Left
            {
                velocity.X = -5;
                isFacingLeft = true;
            }

            if (keyboardState.IsKeyDown(Keys.D)) // Right
            {
                velocity.X = 5;
                isFacingLeft = false;
            }

            if (keyboardState.IsKeyDown(Keys.W)) // Up
                velocity.Y = -5;

            if (keyboardState.IsKeyDown(Keys.S)) // Down
                velocity.Y = 5;
        }
        public void UpdateHitbox()
        {
            rectangle = new Rectangle(position.ToPoint(), rectangle.Size); // Rectangle = Position
            hitbox = new Rectangle((int)position.X, (int)position.Y + 24 * scale, 11 * scale, 4 * scale);
        }

        public void Update(List<Wall> walls, bool isFacingLeft)
        {
            keyboardState = Keyboard.GetState();
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
            {
                walking = false;

                walkLeftAnimation.Reset();
                walkRightAnimation.Reset();
            }

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
            return hitbox.Right + velocity.X > wall.Hitbox.Left &&
              hitbox.Left < wall.Hitbox.Left &&
              hitbox.Bottom > wall.Hitbox.Top &&
              hitbox.Top < wall.Hitbox.Bottom;
        }

        protected bool IsTouchingRight(Wall wall)
        {
            return hitbox.Left + velocity.X < wall.Hitbox.Right &&
              hitbox.Right > wall.Hitbox.Right &&
              hitbox.Bottom > wall.Hitbox.Top &&
              hitbox.Top < wall.Hitbox.Bottom;
        }

        protected bool IsTouchingTop(Wall wall)
        {
            return hitbox.Bottom + velocity.Y > wall.Hitbox.Top &&
              hitbox.Top < wall.Hitbox.Top &&
              hitbox.Right > wall.Hitbox.Left &&
              hitbox.Left < wall.Hitbox.Right;
        }

        protected bool IsTouchingBottom(Wall wall)
        {
            return hitbox.Top + velocity.Y < wall.Hitbox.Bottom &&
              hitbox.Bottom > wall.Hitbox.Bottom &&
              hitbox.Right > wall.Hitbox.Left &&
              hitbox.Left < wall.Hitbox.Right;
        }
    }
}