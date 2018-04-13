using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ScrollShooter
{
    class Game 
    {
        GameInterface gameInterface;
        public Game()
        {
            gameInterface = new GameInterface();
        }
        public void Update()
        {

        }
        public void Draw()
        {
            Program.Window.Draw(gameInterface);
        }
    }
}
