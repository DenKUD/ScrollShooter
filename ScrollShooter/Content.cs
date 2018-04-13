using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
namespace ScrollShooter
{
    class Content
    {
        public const string CONTENT_DIR = "..\\Content";
        public const string TEXTURE_DIR = "\\Textures";
        public const string SHIP_DIR = "\\Ships";
        public const string FONT_DIR = "\\Fonts";
        public static Texture borderTex;
        public static Texture obstacleTex;
        public static Font font;
        public static void Load()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            borderTex = new Texture(CONTENT_DIR + TEXTURE_DIR + "\\MetalTiles.png");
            font = new Font(CONTENT_DIR + FONT_DIR + "\\NeverSurrender.ttf");
        }
    }
    
}
