﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Diagnostics;

namespace ScrollShooter
{
    
    class Ship : Transformable, Drawable
    {
        RectangleShape rectShape;
        public const int SHIP_SIZE= 256;
        public const float SIZE_FACTOR = 0.25f;
        public float shipSpeed = 10f;
        public GameInterface gameInterface;
        public bool isDead;
        public bool isShot;
        TimeSpan coolDown;
        Stopwatch cd;
        
        public Ship(Vector2f position, GameInterface gInterface,float speed)
        {
            isDead = false;
            shipSpeed = speed;
            gameInterface = gInterface;
            Position = position;
            coolDown = new TimeSpan(0,0,0,0,500);
            cd = new Stopwatch();
            cd.Start();
            rectShape = new RectangleShape(new SFML.System.Vector2f(SHIP_SIZE, SHIP_SIZE));
            rectShape.Texture = Content.shipTex;
            rectShape.TextureRect = new IntRect(0, 0, SHIP_SIZE, SHIP_SIZE);
            rectShape.Scale= new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
        }

        public void Update(IEnumerable<Enemy> enemies,IEnumerable<Bullet> bullets)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)&&cd.Elapsed>coolDown&&!isDead)
            {

                isShot = true;
                cd.Restart();
            }
            UpdatePosition();
            foreach (Enemy e in enemies)
                CheckColision(e);
            foreach (Bullet b in bullets)
                CheckColision(b);
        }
        void UpdatePosition()
        {
            if (!isDead)
            {
                var oldPos = this.Position;
                var newPos = new Vector2f(oldPos.X, oldPos.Y);
                bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
                bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
                bool isMoveUp = Keyboard.IsKeyPressed(Keyboard.Key.Up);
                bool isMoveDown = Keyboard.IsKeyPressed(Keyboard.Key.Down);

                if (isMoveLeft) newPos = new Vector2f(Position.X - shipSpeed, Position.Y);
                if (isMoveRight) newPos = new Vector2f(Position.X + shipSpeed, Position.Y);
                if (isMoveUp) newPos = new Vector2f(Position.X, Position.Y - shipSpeed);
                if (isMoveDown) newPos = new Vector2f(Position.X, Position.Y + shipSpeed);
                /*Check intersection with borders newPos - coordinats of upper left conner of the ship
                 so we add half of the ship size*/
                if (gameInterface.GetTile(newPos.X + (float)SHIP_SIZE * SIZE_FACTOR / 2,
                    newPos.Y + (float)SHIP_SIZE * SIZE_FACTOR / 2) == null) Position = newPos;
            }
        }
        void CheckColision(Enemy enemy)
        {
            var enemyPos = enemy.GetPosition();
            var shipPos = new Vector2f(Position.X + SIZE_FACTOR * SHIP_SIZE / 2,
                Position.Y + SIZE_FACTOR * SHIP_SIZE / 2);
            var enemyHalfSize = Enemy.SIZE * Enemy.SIZE_FACTOR / 2;
            var shipHalfSize = SIZE_FACTOR * SHIP_SIZE / 2;
            if (Math.Abs (shipPos.X - enemyPos.X) < (enemyHalfSize + shipHalfSize)/2 &&
                Math.Abs(shipPos.Y - enemyPos.Y) < (enemyHalfSize + shipHalfSize)/2)
                isDead = true;
        }
        void CheckColision(Bullet bullet)
        {
            var bulletPos = bullet.GetPosition();
            var shipPos = new Vector2f(Position.X + SIZE_FACTOR * SHIP_SIZE / 2,
                Position.Y + SIZE_FACTOR * SHIP_SIZE / 2);
            var enemyHalfSize = Bullet.BULLET_SIZE * Bullet.SIZE_FACTOR / 2;
            var shipHalfSize = SIZE_FACTOR * SHIP_SIZE / 2;
            if (Math.Abs(shipPos.X - bulletPos.X) < (enemyHalfSize + shipHalfSize) / 2 &&
                Math.Abs(shipPos.Y - bulletPos.Y) < (enemyHalfSize + shipHalfSize) / 2)
            {
                isDead = true;
                bullet.isDead = true;
            }
        }
        public Bullet Shoot()
        {
            return new Bullet(new Vector2f(Position.X+SHIP_SIZE*SIZE_FACTOR/2-Bullet.BULLET_SIZE/2, Position.Y), new Vector2f(0, -1), 15f);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
    }
}
