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
        Wave cWave;
        Wave wave1;
        Wave wave2;
        Wave wave3;
        int currentWave;
        //Level[] levels;
        //Level cLevel;
        
        
        public Game()
        {
            gameInterface = new GameInterface();
            playerShip = new Ship(new SFML.System.Vector2f(700, 500),gameInterface,14f);
            enemies = new LinkedList<Enemy>();
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(300, 300), gameInterface, 14f));
            enemies.AddLast(new Enemy(new SFML.System.Vector2f(900, 300), gameInterface, 14f));
            wave1 = new Wave(enemies);
            //waves = new LinkedList<Wave>();
            //waves.AddLast(cWave);
            //var wave2 = new Wave(enemies);
            //waves.AddLast(wave2);
            //var level = new Level(waves);
            //levels = ;
            //levels.AddLast(level);
            var enemies2 = new LinkedList<Enemy>();
            enemies2.AddLast(new Enemy(new SFML.System.Vector2f(300, 300), gameInterface, 14f));
            enemies2.AddLast(new Enemy(new SFML.System.Vector2f(700, 300), gameInterface, 14f));
            enemies2.AddLast(new Enemy(new SFML.System.Vector2f(1000, 300), gameInterface, 14f));
            wave2 = new Wave(enemies2);
            var enemies3 = new LinkedList<Enemy>();
            enemies3.AddLast(new Enemy(new SFML.System.Vector2f(600, 300), gameInterface, 14f,true));
            wave3 = new Wave(enemies3);
            playTime = new Stopwatch();
            playerBullets = new LinkedList<Bullet>();
            enemyBullets = new LinkedList<Bullet>();
            playTime.Start();
            bonuses = new LinkedList<Bonus>();
                bonuses.AddLast(new Bonus(BonusType.SHIELD));
            //cLevel = levels.Last();
            // levels.RemoveLast();
            //waves = cLevel.waves;
            // cWave = waves.Last.Value;
            //waves.RemoveLast();
            currentWave = 1;
            cWave = wave1;
        }
        public void Update()
        {
            gameInterface.Time = playTime.Elapsed;
            gameInterface.Update();
            playerShip.Update(cWave.Enemies,enemyBullets,bonuses);
            try
            {
                foreach (var e in cWave.Enemies)
                    e.Update(EnemyMovement.HJitter(playTime.Elapsed,
                    e.Position
                    , 2), playerBullets);
                /*
                cWave.Enemies.Last().Update(
                    EnemyMovement.HJitter(playTime.Elapsed,
                    cWave.Enemies.Last().Position
                    , 2), playerBullets);
                    */
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
            for (int i = 0; i < cWave.Enemies.Count(); i++)
            {

                cWave.Enemies.ElementAt(i).Update(playerBullets);
                if (cWave.Enemies.ElementAt(i).isShot)
                {
                    enemyBullets.AddLast(cWave.Enemies.ElementAt(i).Shoot());
                    cWave.Enemies.ElementAt(i).isShot = false;
                }
                if (cWave.Enemies.ElementAt(i).isDead) cWave.Enemies.Remove(cWave.Enemies.ElementAt(i));
            }
            for (int i = 0; i < bonuses.Count(); i++)
            {
                if (bonuses.ElementAt(i).isPickedUp) bonuses.Remove(bonuses.ElementAt(i));
            }
            //cWave.Update();
            if (cWave.Enemies.Count == 0)
            {
                currentWave++;
                if (currentWave == 2) cWave = wave2;
                if (currentWave == 3) cWave = wave3;
                if (currentWave == 4) gameInterface.win = true;
            }
        }
        public void Draw()
        {
            Program.Window.Draw(playerShip);
            Program.Window.Draw(gameInterface);
            foreach(Enemy e in cWave.Enemies)
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
