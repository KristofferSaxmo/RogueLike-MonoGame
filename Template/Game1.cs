using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Template
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D defaultTex, crosshairTex, gunTex, bulletTex;

        Player player;
        Gun gun;
        List<Bullet> bullets = new List<Bullet>();
        List<Wall> walls = new List<Wall>();

        Rectangle recordPlayerRec;

        KeyboardState keyboardState;
        MouseState mouseState;
        bool isTurnedLeft;

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

            crosshairTex = Content.Load<Texture2D>("crosshair");

            bulletTex = Content.Load<Texture2D>("bullet");
            gunTex = Content.Load<Texture2D>("gunLeft");
            gun = new Gun(gunTex, 1, 30);
            walls.Add(new Wall(defaultTex, new Vector2(400, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(600, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(600, 600), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(800, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(600, 200), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(1000, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(1210, 400), new Point(200, 200)));
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            recordPlayerRec = player.Rectangle;
            player.Update();

            if (mouseState.X < player.Position.X + (player.Rectangle.Width) / 2) // Is the player turned left?
            {
                isTurnedLeft = true; // Yes
                gunTex = Content.Load<Texture2D>("gunLeft");
                gun.Shoot(bullets, bulletTex, 0);
            }
            else
            {
                isTurnedLeft = false; // No
                gunTex = Content.Load<Texture2D>("gunRight");
                gun.Shoot(bullets, bulletTex, gunTex.Width);
            }

            gun.Update(player.Position, gunTex);

            for (int i = 0; i < bullets.Count; i++) // Move bullets
            {
                bullets[i].Move();
            }

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

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (walls[i].Rectangle.Intersects(bullets[j].Rectangle)) // Platform & Bullet collision
                    {
                        bullets.RemoveAt(j);
                        j--;
                    }
                }
            }
            player.UpdateHitbox();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int i = 0; i < bullets.Count; i++) // Draw bullets
            {
                bullets[i].Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw(spriteBatch);
            }

            if (isTurnedLeft == true)
                gun.DrawLeft(spriteBatch); // Draw left gun

            else
                gun.DrawRight(spriteBatch); // Draw right gun

            spriteBatch.Draw(crosshairTex, new Vector2(mouseState.X - 10, mouseState.Y - 10), Color.Black); // Draw crosshair

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
