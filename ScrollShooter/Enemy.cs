using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;

namespace ScrollShooter
{
    enum Movement
    {
        Up, Down, Left, Right, UpLeft,UpRight,DownLeft, DownRight
    }
    class Enemy :  Transformable, Drawable
    {
        RectangleShape rectShape;
        public const int SIZE = 256;
        public const float SIZE_FACTOR = 0.25f;
        public float Speed = 10f;
        public GameInterface gameInterface;
        public bool isDead;
        public bool isShot;
        TimeSpan coolDown;
        Stopwatch cd;
        public Enemy(Vector2f position, GameInterface gInterface, float speed)
        {
            coolDown = new TimeSpan(0, 0, 1);
            Speed = speed;
            gameInterface = gInterface;
            Position = position;
            rectShape = new RectangleShape(new SFML.System.Vector2f(SIZE, SIZE));
            rectShape.Texture = Content.enemyTex1;
            rectShape.TextureRect = new IntRect(0, 0, SIZE, SIZE);
            rectShape.Scale = new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
            cd = new Stopwatch();
            cd.Start();
        }

        public Enemy(Vector2f position, GameInterface gInterface, float speed, bool isBoss)
        {
            coolDown = new TimeSpan(0, 0, 1);
            Speed = speed;
            gameInterface = gInterface;
            Position = position;
            rectShape = new RectangleShape(new SFML.System.Vector2f(SIZE, SIZE));
            rectShape.Texture = Content.bossTex;
            rectShape.TextureRect = new IntRect(0, 0, SIZE*2, SIZE*2);
            rectShape.Scale = new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
            cd = new Stopwatch();
            cd.Start();
        }
        public void Update(SFML.System.Vector2f newPos,IEnumerable<Bullet> bullets)
        {
            UpdatePosition(newPos);
            foreach (Bullet b in bullets)
                CheckColision(b);
        }
        public void Update( IEnumerable<Bullet> bullets)
        {
            foreach (Bullet b in bullets)
                CheckColision(b);
            if (cd.Elapsed > coolDown)
            {

                isShot = true;
                cd.Restart();
            }
        }
        void UpdatePosition(SFML.System.Vector2f newPos)
        {
            var oldPos = this.Position;

            /*Check intersection with borders newPos - coordinats of upper left conner of the ship
             so we add half of the ship size*/
            if (gameInterface.GetTile(newPos.X + (float)SIZE * SIZE_FACTOR / 2,
                newPos.Y + (float)SIZE * SIZE_FACTOR / 2) == null)
            {
                Position = newPos;
            }
            else
            {
                isDead = true;
            }
        }
        void CheckColision(Bullet bullet)
        {
            var bulletPos = bullet.GetPosition();
            var shipPos = new Vector2f(Position.X + SIZE_FACTOR * SIZE / 2,
                Position.Y + SIZE_FACTOR * SIZE / 2);
            var enemyHalfSize = Bullet.BULLET_SIZE * Bullet.SIZE_FACTOR / 2;
            var shipHalfSize = SIZE_FACTOR * SIZE / 2;
            if (Math.Abs(shipPos.X - bulletPos.X) < (enemyHalfSize + shipHalfSize) / 2 &&
                Math.Abs(shipPos.Y - bulletPos.Y) < (enemyHalfSize + shipHalfSize) / 2)
            {
                isDead = true;
                bullet.isDead = true;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
        public Vector2f GetPosition()
        {
            return new Vector2f(Position.X + (float)SIZE * SIZE_FACTOR / 2,
                Position.Y + (float)SIZE * SIZE_FACTOR / 2);
        }
        public Bullet Shoot()
        {
            return new Bullet(new Vector2f(Position.X + SIZE * SIZE_FACTOR / 2 - Bullet.BULLET_SIZE / 2,
                Position.Y+SIZE * SIZE_FACTOR / 2 - Bullet.BULLET_SIZE / 2), new Vector2f(0, 1), 15f);
        }
    }
}
