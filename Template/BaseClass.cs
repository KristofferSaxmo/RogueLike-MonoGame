using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        protected int damage;
        protected int rateOfFire;

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
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int RateOfFire
        {
            get { return rateOfFire; }
            set { rateOfFire = value; }
        }

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}