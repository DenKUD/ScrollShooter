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
        Text _score;
        Text _time;
        Text _gameOver;
        public int Score=0;
        public TimeSpan Time=new TimeSpan(0,0,0);
        public bool gameOver = false;
        public GameInterface()
        {
            _score = new Text("Score", Content.font, Tile.TILE_SIZE * 3);
            _time = new Text("Time", Content.font, Tile.TILE_SIZE * 3);
            _gameOver = new Text("Game Over", Content.font, Tile.TILE_SIZE * 3);
            _score.Position = new SFML.System.Vector2f(1 * Tile.TILE_SIZE,  0);
            _time.Position = new SFML.System.Vector2f(WIDTH/2* Tile.TILE_SIZE,  0);
            _gameOver.Position = new SFML.System.Vector2f(WIDTH / 4  * Tile.TILE_SIZE+ 2*Tile.TILE_SIZE, HEIGHT/2 *Tile.TILE_SIZE);
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
            _score.DisplayedString = "Score " + Score.ToString();
            _time.DisplayedString = "Time " + Time.ToString(@"mm\:ss");
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Update();
            target.Draw(_score);
            target.Draw(_time);
            if (gameOver) target.Draw(_gameOver);
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
