using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace RValley.Client.UI
{
    public class HealthBar
    {
        private Texture2D[] textures;
        private int[] position;
        private int[] HealthBarSizeMax;
        private Rectangle[] rectangles;

        public HealthBar() {
            this.position = new int[2] { 100, 800 };
            

        
        }

        public SpriteBatch Draw(SpriteBatch spriteBatch, Player player) {

            this.rectangles[(int)UIEnums.HealthBar.REDBAR].Width = (int)(((float)this.HealthBarSizeMax[0] / (float)player.hpMax) * player.hp);

            spriteBatch.Draw(this.textures[(int)UIEnums.HealthBar.BG], this.rectangles[0], Microsoft.Xna.Framework.Color.White);

            spriteBatch.Draw(this.textures[(int)UIEnums.HealthBar.REDBAR], this.rectangles[1], Microsoft.Xna.Framework.Color.White);

            return spriteBatch;
        }

        public void LoadContent(Texture2D[] textures) {
            this.textures = textures;
            this.HealthBarSizeMax = new int[2] 
            { 
                this.textures[(int)UIEnums.HealthBar.BG].Width - 10,
                this.textures[(int)UIEnums.HealthBar.BG].Height - 10 
            };
            this.rectangles = new Rectangle[2]
            {      
                new Rectangle(this.position[0], this.position[1], this.textures[(int)UIEnums.HealthBar.BG].Width, this.textures[(int)UIEnums.HealthBar.BG].Height),
                new Rectangle(this.position[0] + 5, this.position[1] + 5, this.HealthBarSizeMax[0], this.HealthBarSizeMax[1])
            };
        }

    }
}
