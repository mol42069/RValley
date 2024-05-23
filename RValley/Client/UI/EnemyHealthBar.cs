using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Entities.Enemies;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RValley.Client.UI
{
    public class EnemyHealthBar
    {
        Texture2D[] texture;
        private int maxSize;
        private int offset, height;

        private Rectangle baseRectangle, rectangle;

        public EnemyHealthBar()
        { 
            this.maxSize = 146;
            this.offset = 2;
            this.height = 15;
        }
        public SpriteBatch Draw(SpriteBatch spriteBatch, List<Enemies> enemy, MapManager mapManager) 
        {
            for (int i = 0; i < enemy.Count; i++) {



                this.baseRectangle = new Rectangle(0, 0, (int)((this.maxSize + 2 * this.offset) * enemy[i].spriteScale), (int)(this.height * enemy[i].spriteScale));
                this.rectangle = new Rectangle((int)(this.offset * enemy[i].spriteScale), (int)(this.offset * enemy[i].spriteScale), (int)(this.maxSize * enemy[i].spriteScale), (int)((15 - this.offset * 2) * enemy[i].spriteScale));

                int[] tempbr = new int[2] 
                { 
                    enemy[i].hitBox.Center.X - (this.baseRectangle.Width / 2),
                    enemy[i].hitBox.Y - 10
                };

                tempbr = mapManager.calculateDrawPositionEntity(tempbr);

                this.baseRectangle.X = tempbr[0] + 15;
                this.baseRectangle.Y = tempbr[1];

                this.rectangle.X = this.baseRectangle.X + this.offset;
                this.rectangle.Y = this.baseRectangle.Y + this.offset;
                //this.rectangle.Width = this.maxSize - (enemy[i].hpMax - enemy[i].hp);
                this.rectangle.Width = (int)((this.maxSize * ((float)enemy[i].hp / (float)enemy[i].hpMax)* enemy[i].spriteScale));

                spriteBatch.Draw(this.texture[0], this.baseRectangle, Color.White);
                spriteBatch.Draw(this.texture[1], this.rectangle, Color.White);

            }


            return spriteBatch;
        }

        public void LoadContent(Texture2D[] sprites)
        { 
            this.texture = sprites;
        }

    }
}
