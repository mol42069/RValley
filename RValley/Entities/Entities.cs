using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities
{

// WE WANT TO DO EVERYTHING (WHAT WE DO IN CHILDREN) WE CAN IN THIS CLASS:
    internal class Entities
    {
        protected int[] position;
        protected int spriteSize;
        protected Rectangle HitBox;   // if we decide to add headshots or other stuff we might want to add this here... or save those somewhere else.
        protected Texture2D[] spriteSheets;
        protected Rectangle[][] sourceRectangle;
        private int aniCounter, aniCounterMax, aniTimer, aniTimerMax;

        public Entities() {

        }

        public virtual void Update() {

        }
        public virtual SpriteBatch Draw(SpriteBatch spriteBatch){


            return spriteBatch;
        }
        public virtual void Movement(int[] move)
        {

        }

        // we only want spriteSheets for one playerClass.
        public void LoadContent(Texture2D[] spriteSheets, Rectangle[][] sourceRectangle) { 
            this.spriteSheets = spriteSheets;
            this.sourceRectangle = sourceRectangle;
        }

        public void LoadContent(Texture2D[] spriteSheets) {
            this.spriteSheets = spriteSheets;
            this.CreatesourceRectangles();
        }

        private void CreatesourceRectangles() 
        {
            this.sourceRectangle = new Rectangle[this.spriteSheets.Length][];
            for (int i = 0; i < this.spriteSheets.Length; i++) 
            {
                this.spriteSize = this.spriteSheets[i].Height;
                this.sourceRectangle[i] = new Rectangle[this.spriteSheets[i].Width / this.spriteSize];

                for (int j = 0; j < this.sourceRectangle[i].Length; j++) 
                {
                    this.sourceRectangle[i][j] = new Rectangle(j * this.spriteSize, 0, spriteSize, spriteSize);                
                }
            }
        }

        public void Animation() {
        
        
        }
    }
}
