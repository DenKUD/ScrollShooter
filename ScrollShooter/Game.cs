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
        Wave wave;
        LinkedList<Level> levels;
        Level cLevel;
        
        public Game()
        {
            gameInterface = new GameInterface();
            playerShip = new Ship(new SFML.System.Vector2f(700, 500),gameInterface,14f);
            enemies = new LinkedList<Enemy>();
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(300, 300), gameInterface, 14f));
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(700, 300), gameInterface, 14f));
            wave = new Wave(enemies);
            var waves = new LinkedList<Wave>();
            waves.AddLast(wave);
            var wave2 = new Wave(enemies);
            waves.AddLast(wave2);
            var level = new Level(waves);
            levels = new LinkedList<Level>();
            levels.AddLast(level);
            playTime = new Stopwatch();
            playerBullets = new LinkedList<Bullet>();
            enemyBullets = new LinkedList<Bullet>();
            playTime.Start();
            bonuses = new LinkedList<Bonus>();
                bonuses.AddLast(new Bonus(BonusType.SHIELD));
            cLevel = levels.Last();
            levels.RemoveLast();
        }
        public void Update()
        {
            gameInterface.Time = playTime.Elapsed;
            gameInterface.Update();
            playerShip.Update(wave.Enemies,enemyBullets,bonuses);
            try
            {
                wave.Enemies.Last().Update(
                    EnemyMovement.HJitter(playTime.Elapsed,
                    wave.Enemies.Last().Position
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
            for (int i = 0; i < wave.Enemies.Count(); i++)
            {

                wave.Enemies.ElementAt(i).Update(playerBullets);
                if (wave.Enemies.ElementAt(i).isShot)
                {
                    enemyBullets.AddLast(wave.Enemies.ElementAt(i).Shoot());
                    wave.Enemies.ElementAt(i).isShot = false;
                }
                if (wave.Enemies.ElementAt(i).isDead) wave.Enemies.Remove(wave.Enemies.ElementAt(i));
            }
            for (int i = 0; i < bonuses.Count(); i++)
            {
                if (bonuses.ElementAt(i).isPickedUp) bonuses.Remove(bonuses.ElementAt(i));
            }
            wave.Update();
            if(!wave.IsAlive)
            {
                wave = cLevel.waves.Last();
                cLevel.waves.RemoveLast();
            }
            cLevel.Update();
            if (!cLevel.IsAlive)
            {
                if (levels.Count <= 0) gameInterface.win = true;
                else
                {
                    cLevel = levels.Last.Value;
                    levels.RemoveLast();
                }
            }
        }
        public void Draw()
        {
            Program.Window.Draw(playerShip);
            Program.Window.Draw(gameInterface);
            foreach(Enemy e in wave.Enemies)
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
