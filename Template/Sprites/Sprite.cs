using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Sprites
{
    class Sprite
    {
        protected Texture2D texture;
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }

        protected Vector2 position;
        protected Vector2 velocity;
        protected Rectangle rectangle;
        protected Rectangle hitbox;
        protected Rectangle sourceRectangle;
        protected Vector2 leftOrigin;
        protected Vector2 rightOrigin;
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
        public Rectangle Hitbox
        {
            get { return hitbox; }
        }
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
        }
        public Vector2 LeftOrigin
        {
            get { return leftOrigin; }
            set { leftOrigin = value; }
        }
        public Vector2 RightOrigin
        {
            get { return rightOrigin; }
            set { rightOrigin = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
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

        public virtual void Draw(SpriteBatch spriteBatch) { }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.hitbox.Right + this.velocity.X > sprite.hitbox.Left &&
              this.hitbox.Left < sprite.hitbox.Left &&
              this.hitbox.Bottom > sprite.hitbox.Top &&
              this.hitbox.Top < sprite.hitbox.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.hitbox.Left + this.velocity.X < sprite.hitbox.Right &&
              this.hitbox.Right > sprite.hitbox.Right &&
              this.hitbox.Bottom > sprite.hitbox.Top &&
              this.hitbox.Top < sprite.hitbox.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.hitbox.Bottom + this.velocity.Y > sprite.hitbox.Top &&
              this.hitbox.Top < sprite.hitbox.Top &&
              this.hitbox.Right > sprite.hitbox.Left &&
              this.hitbox.Left < sprite.hitbox.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.hitbox.Top + this.velocity.Y < sprite.hitbox.Bottom &&
              this.hitbox.Bottom > sprite.hitbox.Bottom &&
              this.hitbox.Right > sprite.hitbox.Left &&
              this.hitbox.Left < sprite.hitbox.Right;
        }
        #endregion
    }
}