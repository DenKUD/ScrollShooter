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
        public const int SHIP_SIZE= 256*256;
        public const float SHIP_SPEED = 10f;

        public Ship(Vector2f position)
        {
            Position = position;
            rectShape = new RectangleShape(new SFML.System.Vector2f(SHIP_SIZE, SHIP_SIZE));
            rectShape.Texture = Content.shipTex;
            rectShape.TextureRect = new IntRect(0, 0, SHIP_SIZE, SHIP_SIZE);
            rectShape.Scale= new Vector2f(0.5f,0.5f);
        }

        public void Update()
        {
            UpdatePosition();
        }
        void UpdatePosition()
        {
            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
            bool isMoveUp = Keyboard.IsKeyPressed(Keyboard.Key.Up);
            bool isMoveDown = Keyboard.IsKeyPressed(Keyboard.Key.Down);

            if (isMoveLeft) Position = new Vector2f(Position.X-SHIP_SPEED,Position.Y);
            if (isMoveRight) Position = new Vector2f(Position.X + SHIP_SPEED, Position.Y);
            if (isMoveUp) Position = new Vector2f(Position.X, Position.Y-SHIP_SPEED);
            if (isMoveDown) Position = new Vector2f(Position.X, Position.Y+SHIP_SPEED);  
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
    }
}
