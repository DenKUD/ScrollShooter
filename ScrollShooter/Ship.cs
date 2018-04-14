using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.IO;
using SFML.Window;

namespace ScrollShooter
{
    
    class Ship : Transformable, Drawable
    {
        RectangleShape rectShape;
        public const int SHIP_SIZE= 256;
        public const float SIZE_FACTOR = 0.25f;
        public float shipSpeed = 10f;
        public GameInterface gameInterface;
        public Ship(Vector2f position, GameInterface gInterface,float speed)
        {
            shipSpeed = speed;
            gameInterface = gInterface;
            Position = position;
            rectShape = new RectangleShape(new SFML.System.Vector2f(SHIP_SIZE, SHIP_SIZE));
            rectShape.Texture = Content.shipTex;
            rectShape.TextureRect = new IntRect(0, 0, SHIP_SIZE, SHIP_SIZE);
            rectShape.Scale= new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
        }

        public void Update()
        {
            UpdatePosition();
        }
        void UpdatePosition()
        {
            var oldPos = this.Position;
            var newPos = new Vector2f(oldPos.X,oldPos.Y);
            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
            bool isMoveUp = Keyboard.IsKeyPressed(Keyboard.Key.Up);
            bool isMoveDown = Keyboard.IsKeyPressed(Keyboard.Key.Down);

            if (isMoveLeft) newPos = new Vector2f(Position.X- shipSpeed, Position.Y);
            if (isMoveRight) newPos = new Vector2f(Position.X + shipSpeed, Position.Y);
            if (isMoveUp) newPos = new Vector2f(Position.X, Position.Y- shipSpeed);
            if (isMoveDown) newPos = new Vector2f(Position.X, Position.Y+ shipSpeed);
            /*Check intersection with borders newPos - coordinats of upper left conner of the ship
             so we add half of the ship size*/
            if (gameInterface.GetTile(newPos.X + (float)SHIP_SIZE*SIZE_FACTOR/2, 
                newPos.Y + (float)SHIP_SIZE * SIZE_FACTOR / 2) == null) Position = newPos;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
    }
}
