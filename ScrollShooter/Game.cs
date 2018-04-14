using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using System.Diagnostics;

namespace ScrollShooter
{
    class Game 
    {
        GameInterface gameInterface;
        Ship playerShip;
        Stopwatch playTime;
        public Game()
        {
            gameInterface = new GameInterface();
            playerShip = new Ship(new SFML.System.Vector2f(500, 500),gameInterface,14f);
            playTime = new Stopwatch();
            playTime.Start();
        }
        public void Update()
        {
            gameInterface.Time = playTime.Elapsed;
            gameInterface.Update();
            playerShip.Update();
        }
        public void Draw()
        {
            Program.Window.Draw(playerShip);
            Program.Window.Draw(gameInterface);
        }
    }
}
