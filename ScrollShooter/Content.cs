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
        const string CONTENT_DIR = "..\\Content";
        const string TEXTURE_DIR = "\\Textures";
        const string SHIP_DIR = "\\Ships";
        const string FONT_DIR = "\\Fonts";
        public static Texture borderTex;
        public static Texture obstacleTex;
        public static Texture shipTex;
        public static Font font;
        public static void Load()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            borderTex = new Texture(CONTENT_DIR + TEXTURE_DIR + "\\MetalTiles.png");
            font = new Font(CONTENT_DIR + FONT_DIR + "\\NeverSurrender.ttf");
            shipTex=new Texture(CONTENT_DIR+SHIP_DIR+ "\\ship_025.png");
        }
    }
    
}
