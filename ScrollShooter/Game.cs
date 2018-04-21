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
        LinkedList<Bullet> playerBullets;
        LinkedList<Bullet> enemyBullets;
        LinkedList <Bonus> bonuses;
        public Game()
        {
            gameInterface = new GameInterface();
            playerShip = new Ship(new SFML.System.Vector2f(700, 500),gameInterface,14f);
            enemies = new LinkedList<Enemy>();
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(300, 300), gameInterface, 14f));
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(700, 300), gameInterface, 14f));
            playTime = new Stopwatch();
            playerBullets = new LinkedList<Bullet>();
            enemyBullets = new LinkedList<Bullet>();
            playTime.Start();
            bonuses = new LinkedList<Bonus>();
                bonuses.AddLast(new Bonus(BonusType.SHIELD));
        }
        public void Update()
        {
            gameInterface.Time = playTime.Elapsed;
            gameInterface.Update();
            playerShip.Update(enemies,enemyBullets,bonuses);
            try
            {
                enemies.Last().Update(
                    EnemyMovement.HJitter(playTime.Elapsed,
                     enemies.Last().Position
                    , 2), playerBullets);
            }
            catch(InvalidOperationException)
            { }
            //enemy.Update();
            if (playerShip.isDead)
            {
                gameInterface.gameOver = true;
                playTime.Stop();
            }
            if (playerShip.isShot)
            {
                playerShip.isShot = false;
                playerBullets.AddLast(playerShip.Shoot());
            }
            for(int i=0;i<playerBullets.Count();i++)
            {
                playerBullets.ElementAt(i).Update();
                if (playerBullets.ElementAt(i).isDead) playerBullets.Remove(playerBullets.ElementAt(i));
            }
            for (int i = 0; i < enemyBullets.Count(); i++)
            {
                enemyBullets.ElementAt(i).Update();
                
                if (enemyBullets.ElementAt(i).isDead) enemyBullets.Remove(enemyBullets.ElementAt(i));
            }
            for (int i = 0; i < enemies.Count(); i++)
            {
                
                enemies.ElementAt(i).Update(playerBullets);
                if (enemies.ElementAt(i).isShot)
                {
                    enemyBullets.AddLast(enemies.ElementAt(i).Shoot());
                    enemies.ElementAt(i).isShot = false;
                }
                if (enemies.ElementAt(i).isDead) enemies.Remove(enemies.ElementAt(i));
            }
            for (int i = 0; i < bonuses.Count(); i++)
            {
                if (bonuses.ElementAt(i).isPickedUp) bonuses.Remove(bonuses.ElementAt(i));
            }
        }
        public void Draw()
        {
            Program.Window.Draw(playerShip);
            Program.Window.Draw(gameInterface);
            foreach(Enemy e in enemies)
                Program.Window.Draw(e);
            foreach (Bullet b in playerBullets)
                Program.Window.Draw(b);
            foreach (Bullet b in enemyBullets)
                Program.Window.Draw(b);
            foreach (Bonus b in bonuses)
                Program.Window.Draw(b);
        }
    }
}
