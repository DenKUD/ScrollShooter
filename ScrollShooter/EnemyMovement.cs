using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace ScrollShooter
{
    static class EnemyMovement
    {
        public static Vector2f HJitter(TimeSpan ts,Vector2f origin,float A)
        {
            var dx = (float)Math.Cos(ts.TotalSeconds * Math.PI / 0.5) * A;
            return new Vector2f(origin.X + dx, origin.Y);
        }

        public static Vector2f VJitter(TimeSpan ts, Vector2f origin, float A)
        {
            var dy = (float)Math.Cos(ts.TotalSeconds * Math.PI / 5) * A;
            return new Vector2f(origin.X, origin.Y+dy);
        }
    }
}
