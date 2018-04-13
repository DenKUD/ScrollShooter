using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace ScrollShooter
{

    class Program
    {
        public static RenderWindow Window;
        public static Game Game;
        static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(1600, 900), "Bullet: Rain");
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += winCloses;
           // Window.Resized += winResized;
            Content.Load();
            Game = new Game();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();
                Window.Clear(Color.Black);
                Game.Update();
                Game.Draw();
                Window.Display();

            }
        }
        private static void winCloses(object sender, EventArgs args)
        {
            Window.Close();
        }
        private static void winResized(object sender, SFML.Window.SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }
    }
}
