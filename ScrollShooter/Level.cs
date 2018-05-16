using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollShooter
{
    class Level
    {
        public LinkedList<Wave> waves;
        public bool IsAlive;
        public Level(LinkedList<Wave> ws)
        {
            waves = new LinkedList<Wave>(ws);
            IsAlive = true;
        }
        public void Update()
        {
            if (waves.Count <= 0) IsAlive = false;
        }
    }
}
