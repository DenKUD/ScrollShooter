using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace ScrollShooter
{
     class Wave : Transformable, Drawable
    {
        public LinkedList<Enemy> Enemies;
        public bool IsAlive;
        

        public Wave(LinkedList<Enemy> e)
        {
            Enemies = new LinkedList<Enemy>(e);
            IsAlive = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Enemy e in Enemies)
            {
                states.Transform *= Transform;
                e.Draw(target, states);
            }
        }

        public void Update()
        {
            if (Enemies.Count <= 0) IsAlive = false;

        }
    }
}
