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
        private int offset;

        private Rectangle baseRectangle, rectangle;

        public EnemyHealthBar()
        { 
            this.maxSize = 190;
            this.offset = 5;
            this.rectangle = new Rectangle(0, 0, 200, 25);
            this.baseRectangle = new Rectangle(this.offset, this.offset, this.maxSize, 25 - this.offset * 2);
        }
        public SpriteBatch Draw(SpriteBatch spriteBatch, List<Enemies> enemy, MapManager mapManager) 
        {
            for (int i = 0; i < enemy.Count; i++) {
                int[] tempbr = new int[2] 
                { 
                    enemy[i].hitBox.X - this.baseRectangle.Width / 2,
                    enemy[i].hitBox.Y - enemy[i].hitBox.Height / 2
                };

                tempbr = mapManager.calculateDrawPositionEntity(tempbr);

                this.baseRectangle.X = tempbr[0];
                this.baseRectangle.Y = tempbr[1];

                this.rectangle.X = this.baseRectangle.X + this.offset;
                this.rectangle.Y = this.baseRectangle.Y + this.offset;
                this.rectangle.Width = this.maxSize - (enemy[i].hpMax - enemy[i].hp);

                spriteBatch.Draw(this.texture[0], this.rectangle, Color.White);
                spriteBatch.Draw(this.texture[1], this.baseRectangle, Color.White);

            }


            return spriteBatch;
        }

        public void LoadContent(Texture2D[] sprites)
        { 
            this.texture = sprites;
        }

    }
}
