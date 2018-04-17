using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ScrollShooter
{
    class Bullet : Transformable, Drawable
    {
        RectangleShape rectShape;
        public const int BULLET_SIZE = 16;
        public const float SIZE_FACTOR = 1f;
        public float bulletSpeed = 10f;
        public Vector2f bulletDirection;
        public bool isDead;

        public Bullet(Vector2f position,Vector2f direction, float speed)
        {
            isDead = false;
            bulletSpeed = speed;
            Position = position;
            bulletDirection = direction;
            rectShape = new RectangleShape(new SFML.System.Vector2f(BULLET_SIZE, BULLET_SIZE));
            rectShape.Texture = Content.bulletTex;
            rectShape.TextureRect = new IntRect(0, 0, BULLET_SIZE, BULLET_SIZE);
            rectShape.Scale = new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
        }

        public void Update()
        {
            UpdatePosition();
        }
        void UpdatePosition()
        {
            Position = Position + bulletSpeed *  bulletDirection;
            if (Position.X < 0 || Position.Y < 0 || Position.X > 1600 || Position.Y > 900)
                isDead = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(Position.X + (float)BULLET_SIZE * SIZE_FACTOR / 2,
                Position.Y + (float)BULLET_SIZE * SIZE_FACTOR / 2);
        }
    }
}
