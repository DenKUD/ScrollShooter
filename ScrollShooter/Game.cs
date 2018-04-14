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
        LinkedList<Enemy> enemies;
        public Game()
        {
            gameInterface = new GameInterface();
            playerShip = new Ship(new SFML.System.Vector2f(500, 500),gameInterface,14f);
            enemies = new LinkedList<Enemy>();
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(300, 300), gameInterface, 14f));
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(700, 300), gameInterface, 14f));
            playTime = new Stopwatch();
            playTime.Start();
        }
        public void Update()
        {
            gameInterface.Time = playTime.Elapsed;
            gameInterface.Update();
            playerShip.Update(enemies);
            //enemy.Update();
            if (playerShip.isDead)
            {
                gameInterface.gameOver = true;
                playTime.Stop();
            }
        }
        public void Draw()
        {
            Program.Window.Draw(playerShip);
            Program.Window.Draw(gameInterface);
            foreach(Enemy e in enemies)
                Program.Window.Draw(e);
        }
    }
}
