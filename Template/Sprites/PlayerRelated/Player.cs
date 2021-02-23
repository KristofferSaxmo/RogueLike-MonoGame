﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Template.Sprites.WorldGen;

namespace Template.Sprites
{
    class Player : Sprite
    {
        public Player(Texture2D texture, Vector2 position, Point size) : base(texture)
        {
            base.position = position;
            rectangle = new Rectangle(position.ToPoint(), size);
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
        }

        public void Update(List<Wall> walls)
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

            position += velocity;

            velocity = Vector2.Zero;

            UpdateHitbox();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.Purple);
        }

        protected bool IsTouchingLeft(Wall wall)
        {
            return rectangle.Right + velocity.X > wall.Rectangle.Left &&
              rectangle.Left < wall.Rectangle.Left &&
              rectangle.Bottom > wall.Rectangle.Top &&
              rectangle.Top < wall.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Wall wall)
        {
            return rectangle.Left + velocity.X < wall.Rectangle.Right &&
              rectangle.Right > wall.Rectangle.Right &&
              rectangle.Bottom > wall.Rectangle.Top &&
              rectangle.Top < wall.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Wall wall)
        {
            return rectangle.Bottom + velocity.Y > wall.Rectangle.Top &&
              rectangle.Top < wall.Rectangle.Top &&
              rectangle.Right > wall.Rectangle.Left &&
              rectangle.Left < wall.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Wall wall)
        {
            return rectangle.Top + velocity.Y < wall.Rectangle.Bottom &&
              rectangle.Bottom > wall.Rectangle.Bottom &&
              rectangle.Right > wall.Rectangle.Left &&
              rectangle.Left < wall.Rectangle.Right;
        }
    }
}