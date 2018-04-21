using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace ScrollShooter
{
    enum BonusType
    {
        SHIELD,
        OTHER
    }
    class Bonus : Transformable, Drawable
    {
        RectangleShape rectShape;
        public const int BONUS_SIZE = 24;
        public const float SIZE_FACTOR = 1f;
        public bool isPickedUp;
        public BonusType type = BonusType.SHIELD;
        
        public Bonus(BonusType btype)
        {
            type = btype;
            isPickedUp = false;
            var random = new Random();
            
            Position = new Vector2f
                (
                Convert.ToSingle(random.Next(300,600)),
                Convert.ToSingle(random.Next(400, 600))
                );
            rectShape = new RectangleShape(new SFML.System.Vector2f(BONUS_SIZE, BONUS_SIZE));
            rectShape.Texture = Content.shieldTex;
            rectShape.TextureRect = new IntRect(2* BONUS_SIZE, BONUS_SIZE, BONUS_SIZE, BONUS_SIZE);
            rectShape.Scale = new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
        }

        public Bonus(BonusType btype,Vector2f position)
        {
            type = btype;
            isPickedUp = false;
            var random = new Random();

            Position = position;
            rectShape = new RectangleShape(new SFML.System.Vector2f(BONUS_SIZE, BONUS_SIZE));
            rectShape.Texture = Content.shieldTex;
            rectShape.TextureRect = new IntRect(2 * BONUS_SIZE, BONUS_SIZE, BONUS_SIZE, BONUS_SIZE);
            rectShape.Scale = new Vector2f(SIZE_FACTOR, SIZE_FACTOR);
        }

        public Vector2f GetPosition()
        {
            return new Vector2f(Position.X + (float)BONUS_SIZE * SIZE_FACTOR / 2,
                Position.Y + (float)BONUS_SIZE * SIZE_FACTOR / 2);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
    }
}
