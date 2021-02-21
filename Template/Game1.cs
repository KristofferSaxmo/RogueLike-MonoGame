using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Template.Sprites;

namespace Template
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color grass = new Color(36, 73, 67);

        Texture2D defaultTex, crosshairTex, gunTex, bulletTex, telepad_baseTex, telepad_crystalTex;
        Player player;
        Gun gun;
        Telepad_Base telepad_base;
        Telepad_Crystal telepad_crystal;
        Camera camera;
        List<Bullet> bullets = new List<Bullet>();
        List<Wall> walls = new List<Wall>();

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
            camera = new Camera(GraphicsDevice.Viewport);
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            defaultTex = new Texture2D(GraphicsDevice, 1, 1);
            defaultTex.SetData(new Color[1] { Color.White });
            
            player = new Player(defaultTex, new Vector2(0, 0), new Point(30, 30));

            crosshairTex = Content.Load<Texture2D>("crosshair");

            bulletTex = Content.Load<Texture2D>("bullet");

            gunTex = Content.Load<Texture2D>("gunLeft");
            gun = new Gun(gunTex, 1, 30);

            telepad_baseTex = Content.Load<Texture2D>("telepad_base-sheet");
            telepad_base = new Telepad_Base(telepad_baseTex, new Vector2(200, 200), 20, 1, 4, 2);

            telepad_crystalTex = Content.Load<Texture2D>("telepad_crystal-sheet");
            telepad_crystal = new Telepad_Crystal(telepad_crystalTex, new Vector2(200, 200), 20, 1, 4, 2);

            //walls.Add(new Wall(defaultTex, new Vector2(400, 400), new Point(200, 200)));
            //walls.Add(new Wall(defaultTex, new Vector2(600, 400), new Point(200, 200)));
            //walls.Add(new Wall(defaultTex, new Vector2(600, 600), new Point(200, 200)));
            //walls.Add(new Wall(defaultTex, new Vector2(800, 400), new Point(200, 200)));
            //walls.Add(new Wall(defaultTex, new Vector2(600, 200), new Point(200, 200)));
            //walls.Add(new Wall(defaultTex, new Vector2(1000, 400), new Point(200, 200)));
            //walls.Add(new Wall(defaultTex, new Vector2(1225, 400), new Point(200, 200)));
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            player.Update(walls);

            if (camera.GetWorldPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)).X < player.Position.X + (player.Rectangle.Width) / 2) // Is the player turned left?
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

            for (int i = 0; i < bullets.Count; i++) // Move bullets
            {
                bullets[i].Move();
            }

            for (int i = 0; i < walls.Count; i++) // Kollision med väggar
            {
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

            gun.Update(camera, camera.Position, player.Position, gunTex);

            telepad_base.Update();
            telepad_crystal.Update();

            camera.MoveCamera(player.Position);

            camera.UpdateCamera(GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(grass); // Draw background

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            telepad_base.Draw(spriteBatch); // Draw telepad base

            player.Draw(spriteBatch); // Draw player

            if (isTurnedLeft == true)
                gun.DrawLeft(spriteBatch); // Draw left gun

            else
                gun.DrawRight(spriteBatch); // Draw right gun     

            telepad_crystal.Draw(spriteBatch);

            for (int i = 0; i < bullets.Count; i++) // Draw bullets
            {
                bullets[i].Draw(spriteBatch);
            }

            for (int i = 0; i < walls.Count; i++) // Draw walls
            {
                walls[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin();

            spriteBatch.Draw(crosshairTex, new Vector2(mouseState.X - 10, mouseState.Y - 10), Color.Black); // Draw crosshair

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
