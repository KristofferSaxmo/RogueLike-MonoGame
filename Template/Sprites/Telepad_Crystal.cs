﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Sprites
{
    class Telepad_Crystal : BaseClass
    {
        private Animation animation;
        public Telepad_Crystal(Texture2D texture, Vector2 position, int frameTime, int rows, int cols, int scale)
        {
            animation = new Animation(texture, position, frameTime, rows, cols, scale);
            this.position = position;
        }
        public void Update()
        {
            animation.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, position);
        }
    }
}