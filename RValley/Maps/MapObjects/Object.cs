using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Maps.MapObjects
{
    internal class Object
    {
        private bool solid, breakable, breakAniFinish;
        public int[] pos;
        public Texture2D[] spritesheet;
        private Rectangle[] sourceRectangles;
        public Rectangle rect, drawRect;
        public int hp;

        public Object(int[] pos, Texture2D[] spritesheet, bool solid=true, bool breakable=false, int hp = 1)
        {
            this.solid = solid;
            this.breakable = breakable;
            this.breakAniFinish = false;
            this.pos = pos;
            this.hp = hp;
            this.spritesheet = spritesheet;
            this.createSourceRectangles();
        }

        public void LoadContent(Texture2D[] spritesheet)
        {
            this.spritesheet = spritesheet;
            this.sourceRectangles = new Rectangle[spritesheet.Length];
            for (int i = 0; i < spritesheet.Length; i++)
            {
                this.sourceRectangles[i] = new Rectangle(0, 0, spritesheet[i].Width, spritesheet[i].Height);
            }
            this.rect = new Rectangle(this.pos[0], this.pos[1], this.sourceRectangles[0].Width, this.sourceRectangles[0].Height);
            this.drawRect = this.rect;
        }

        public bool Update()
        { // true wenn object gelöscht werden soll/es gebrochen wurde.
            if (this.hp <= 0 && this.breakAniFinish) return true;
            else return false;
        }

        public SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager)
        {
            int[] tempPos = mapManager.calculateDrawPositionEntity(this.pos);
            this.drawRect.X = tempPos[0];
            this.drawRect.Y = tempPos[1];
            for (int i = 0; i < this.spritesheet.Length; i++)
            {
                spriteBatch.Draw(this.spritesheet[i], this.rect, this.sourceRectangles[i], Color.White);
            }
            return spriteBatch;
        }

        public void BreakObj(int damage, int[] breakPoint)
        {
            Point bPoint = new Point(breakPoint[0], breakPoint[1]);
            if (this.breakable && this.rect.Contains(bPoint)) this.hp -= damage;
        }

        public bool CollisionCheck(Rectangle rect)
        {
            return this.rect.Intersects(rect);
        }

        private void createSourceRectangles()
        {
            this.sourceRectangles = new Rectangle[this.spritesheet.Length];
            for (int i = 0; i < this.spritesheet.Length; i++)
            {
                this.sourceRectangles[i] = new Rectangle(i * rect.Width, 0, this.rect.Width, this.spritesheet[i].Height);
            }
        }
    }
}
