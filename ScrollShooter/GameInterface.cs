using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ScrollShooter
{
    class GameInterface : Transformable, Drawable
    {
        public uint WIDTH = Program.Window.Size.X/Tile.TILE_SIZE;
        public uint HEIGHT = Program.Window.Size.Y/Tile.TILE_SIZE;
        Tile[][] tiles;
        Text _Score;
        Text _Time;
        public int Score=0;
        public TimeSpan Time=new TimeSpan(0,0,0);
        public GameInterface()
        {
            _Score = new Text("Score", Content.font, Tile.TILE_SIZE * 3);
            _Time = new Text("Time", Content.font, Tile.TILE_SIZE * 3);
            _Score.Position = new SFML.System.Vector2f(1 * Tile.TILE_SIZE,  0);
            _Time.Position = new SFML.System.Vector2f(WIDTH/2* Tile.TILE_SIZE,  0);
            tiles = new Tile[WIDTH][];
            for (int i = 0; i < WIDTH; i++)
                tiles[i] = new Tile[HEIGHT];
            //Draw borders
            for( int x=0;x<WIDTH;x++)
                for(int y=0; y<HEIGHT;y++)
                {
                    if ((x < 7&&y>3)||x==0|| y == 0||(x>WIDTH-8&&y>3)||x==WIDTH-1 || y==HEIGHT-1|| y==3)
                    {
                        this.tiles[x][y] = new Tile(TileType.BORDER);
                        tiles[x][y].Position = new SFML.System.Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE);
                    }
                 
                }
            
        }
        public void Update()
        {
            _Score.DisplayedString = "Score " + Score.ToString();
            _Time.DisplayedString = "Time " + Time.ToString(@"mm\:ss");
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Update();
            target.Draw(_Score);
            target.Draw(_Time);
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    if (tiles[x][y] == null) continue;
                    target.Draw(tiles[x][y]);
                }
            }
        }
        public Tile GetTile( float x, float y)
        {
            int tileX = (int) x / Tile.TILE_SIZE;
            int tileY = (int) y / Tile.TILE_SIZE;
            return tiles[tileX][tileY];
        }
    }
}
