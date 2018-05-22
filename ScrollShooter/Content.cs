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
        const string BULLET_DIR = "\\Bullets";
        public static Texture borderTex;
        public static Texture bulletTex;
        public static Texture shipTex;
        public static Texture enemyTex1;
        public static Texture shieldTex;
        public static Texture ShipShieldTex;
        public static Texture bossTex;
        public static Font font;
        public static void Load()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            borderTex = new Texture(CONTENT_DIR + TEXTURE_DIR + "\\MetalTiles.png");
            font = new Font(CONTENT_DIR + FONT_DIR + "\\NeverSurrender.ttf");
            shipTex=new Texture(CONTENT_DIR+SHIP_DIR+ "\\ship_025.png");
            enemyTex1 = new Texture(CONTENT_DIR + SHIP_DIR + "\\ship_016.png");
            bulletTex = new Texture(CONTENT_DIR + BULLET_DIR + "\\bullet01.png");
            shieldTex = new Texture(CONTENT_DIR + TEXTURE_DIR + "\\BonusTiles.png");
            ShipShieldTex = new Texture(CONTENT_DIR + TEXTURE_DIR + "\\sh_0.png");
            bossTex = new Texture(CONTENT_DIR + SHIP_DIR + "\\Boss.png");
        }
    }
    
}
