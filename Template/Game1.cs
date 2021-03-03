using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Template.Sprites;
using Template.Sprites.PlayerRelated;
using Template.Sprites.WorldGen;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Template
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color grass = new Color(36, 73, 67);

        Texture2D defaultTex, playerIdleLeftTex, playerIdleRightTex, playerWalkLeftTex, playerWalkRightTex, playerShadowTex, crosshairTex, gunLeftTex, gunRightTex, bulletTex, telepad_baseTex, telepad_crystalTex;
        Player player;
        Player_Shadow playerShadow;
        Gun gun;
        Telepad_Base telepadBase;
        Telepad_Crystal telepadCrystal;
        Camera camera;
        List<Bullet> bullets = new List<Bullet>();
        List<Wall> walls = new List<Wall>();

        KeyboardState keyboardState;
        MouseState mouseState;
        bool isFacingLeft = true;

        //Sound
        SoundEffect effect;

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

            playerIdleLeftTex = Content.Load<Texture2D>("player_idle_left");
            playerIdleRightTex = Content.Load<Texture2D>("player_idle_right");
            playerWalkLeftTex = Content.Load<Texture2D>("player_walk_left");
            playerWalkRightTex = Content.Load<Texture2D>("player_walk_right");
            
            playerShadowTex = Content.Load<Texture2D>("player-shadow");

            player = new Player(playerIdleLeftTex, playerWalkLeftTex, playerWalkRightTex, new Vector2(0, 0), 10, 1, 4);

            playerShadow = new Player_Shadow(playerShadowTex, 3);

            crosshairTex = Content.Load<Texture2D>("crosshair");

            bulletTex = Content.Load<Texture2D>("bullet");

            gunLeftTex = Content.Load<Texture2D>("gunLeft");
            gunRightTex = Content.Load<Texture2D>("gunRight");
            gun = new Gun(gunLeftTex, 1, 30);

            telepad_baseTex = Content.Load<Texture2D>("telepad_base-sheet");
            telepadBase = new Telepad_Base(telepad_baseTex, new Vector2(0, 0), 30, 1, 4);

            telepad_crystalTex = Content.Load<Texture2D>("telepad_crystal-sheet");
            telepadCrystal = new Telepad_Crystal(telepad_crystalTex, new Vector2(0, 0), 15, 1, 4);

            walls.Add(new Wall(defaultTex, new Vector2(400, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(600, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(600, 600), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(800, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(600, 200), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(1000, 400), new Point(200, 200)));
            walls.Add(new Wall(defaultTex, new Vector2(1225, 400), new Point(200, 200)));

         
            Song song = Content.Load<Song>("BackgroundMusic");
            song = Content.Load<Song>("BackgroundMusic");
            MediaPlayer.Volume -= 0.96f;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            effect = Content.Load<SoundEffect>("Gunshot");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            player.Update(walls, isFacingLeft);
            playerShadow.Update(player.Position);

            if (camera.GetWorldPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)).X < player.Position.X + (player.Rectangle.Width) / 2) // Is the player turned left?
            {
                isFacingLeft = true; // Yes
                gun.Shoot(bullets, bulletTex, 0, effect);
            }
            else
            {
                isFacingLeft = false; // No
                gun.Shoot(bullets, bulletTex, gunRightTex.Width, effect);
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

            if(isFacingLeft)
                gun.Update(camera, player.Position, gunLeftTex);
            else
                gun.Update(camera, player.Position, gunRightTex);

            telepadBase.Update();
            telepadCrystal.Update();

            camera.MoveCamera(player.Position);

            camera.UpdateCamera(GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(grass); // Draw background

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);

            playerShadow.Draw(spriteBatch); // Draw player shadow

            telepadBase.Draw(spriteBatch); // Draw telepad base

            for (int i = 0; i < walls.Count; i++) // Draw walls
            {
                walls[i].Draw(spriteBatch);
            }

            if (isFacingLeft)
            {
                player.Draw(spriteBatch, playerIdleLeftTex); // Draw left player
                gun.DrawLeft(spriteBatch, gunLeftTex); // Draw left gun
            }
            else
            {
                player.Draw(spriteBatch, playerIdleRightTex); // Draw right player
                gun.DrawRight(spriteBatch, gunRightTex); // Draw right gun   
            }
  

            telepadCrystal.Draw(spriteBatch);

            for (int i = 0; i < bullets.Count; i++) // Draw bullets
            {
                bullets[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin();

            spriteBatch.Draw(crosshairTex, new Vector2(mouseState.X - 10, mouseState.Y - 10), Color.Black); // Draw crosshair

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
