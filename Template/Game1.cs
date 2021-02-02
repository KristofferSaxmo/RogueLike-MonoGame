using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Template
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D defaultTex;

        Player player;
        List<Wall> walls = new List<Wall>();

        Vector2 recordPlayerPos;

        KeyboardState keyboardState = Keyboard.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            defaultTex = new Texture2D(GraphicsDevice, 1, 1);
            defaultTex.SetData(new Color[1] { Color.White });
            
            player = new Player(defaultTex, new Vector2(500, 100), new Point(30, 30));
            walls.Add(new Wall(defaultTex, new Vector2(200, 200), new Point(500, 500)));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            recordPlayerPos = player.Position;
            player.Update();
            for (int i = 0; i < walls.Count; i++) // Kollision med väggar
            {
                if (player.Rectangle.Intersects(walls[i].Rectangle) && recordPlayerRec.Left < walls[i].Rectangle.Left) // Left wall
                    player.Position = new Vector2(
                        walls[i].Rectangle.Left - player.Rectangle.Width, // X
                        player.Position.Y); // Y

                else if (player.Rectangle.Intersects(walls[i].Rectangle) && recordPlayerRec.Right > walls[i].Rectangle.Right) // Right wall
                    player.Position = new Vector2(
                        walls[i].Rectangle.Right, // X
                        player.Position.Y); // Y

                else if (player.Rectangle.Intersects(walls[i].Rectangle) && recordPlayerRec.Top < walls[i].Rectangle.Top) // Top Wall
                    player.Position = new Vector2(
                        player.Position.X, // X
                        walls[i].Rectangle.Top - player.Rectangle.Height); // Y

                else if (player.Rectangle.Intersects(walls[i].Rectangle) && recordPlayerRec.Bottom > walls[i].Rectangle.Bottom) // Bottom Wall
                    player.Position = new Vector2(
                        player.Position.X, // X
                        walls[i].Rectangle.Bottom); // Y
            }
            player.UpdateHitbox();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);

            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
