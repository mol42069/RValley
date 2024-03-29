using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Client.UI
{
    public class UI
    {
        HealthBar healthBar;
        Texture2D[][] textures;
        public UI() {  // this function exists to manage all the basic ui stuff.
            this.healthBar = new HealthBar();

        }

        public SpriteBatch Draw(SpriteBatch spriteBatch, Player player)
        {           // since we are basicly not doing anything in update we
                    // probably just want to update the UI in this draw function.

            spriteBatch = this.healthBar.Draw(spriteBatch, player);


            return spriteBatch;
        }

        public void LoadContent(Texture2D[][] textures) {
            this.textures = textures;
            this.healthBar.LoadContent(this.textures[(int)UIEnums.UIElement.HEALTHBAR]);



        }

    }
}
