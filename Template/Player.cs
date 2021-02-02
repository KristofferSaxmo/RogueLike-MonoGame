using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    class Player : BaseClass
    {

        public Player(Texture2D playerTex, Vector2 playerPos, Point size)
        {
            texture = playerTex;
            position = playerPos;
            rectangle = new Rectangle(playerPos.ToPoint(), size);
        }
        public void MovePlayer()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A)) // Left
            {
                position.X -= 5;
            }

            if (keyboardState.IsKeyDown(Keys.D)) // Right
            {
                position.X += 5;
            }

            if (keyboardState.IsKeyDown(Keys.W)) // Up
            {
                position.Y -= 5;
            }

            if (keyboardState.IsKeyDown(Keys.S)) // Down
            {
                position.Y += 5;
            }
        }
        public void UpdateHitbox()
        {
            rectangle = new Rectangle(position.ToPoint(), rectangle.Size); // Rectangle = Position
        }
        public void Dash()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {

            }
        }

        public override void Update()
        {
            MovePlayer();

            Dash();

            UpdateHitbox();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.Purple);
        }
    }
}