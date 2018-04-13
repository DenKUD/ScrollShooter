using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ScrollShooter
{
    enum TileType
    {
        NONE,
        BORDER,
        OBSTACLE
    }
    class Tile : Transformable, Drawable
    {
        public const int TILE_SIZE = 32;
        TileType type = TileType.NONE;
        RectangleShape rectShape;

        public Tile(TileType tileType)
        {
            type = tileType;
            rectShape = new RectangleShape(new SFML.System.Vector2f(TILE_SIZE, TILE_SIZE));
            switch (type)
            {
                case TileType.BORDER:
                    rectShape.Texture = Content.borderTex;
                    rectShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
                case TileType.OBSTACLE:
                    rectShape.Texture = Content.obstacleTex;
                    rectShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);

        }
    }
}
