using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

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
        public Enemy(Vector2f position, GameInterface gInterface, float speed)
        {
            Speed = speed;
            gameInterface = gInterface;
            Position = position;
            rectShape = new RectangleShape(new SFML.System.Vector2f(SIZE, SIZE));
            rectShape.Texture = Content.enemyTex1;
            rectShape.TextureRect = new IntRect(0, 0, SIZE, SIZE);
            rectShape.Scale = new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
        }

        public void Update(SFML.System.Vector2f newPos)
        {
            UpdatePosition(newPos);
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
    }
}
