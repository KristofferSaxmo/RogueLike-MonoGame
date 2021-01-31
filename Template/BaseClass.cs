using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class BaseClass
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;
        protected float rotation;
        protected Vector2 direction;
        protected float speed;
        protected int health;

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}