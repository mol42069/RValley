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
        protected int[] position, lastMovement;
        protected int spriteSize;
        protected Rectangle hitBox;   // if we decide to add headshots or other stuff we might want to add this here... or save those somewhere else.
        protected Rectangle drawBox;
        public Texture2D[] spriteSheets;
        protected Rectangle[][] sourceRectangle;
        protected int aniCounter, aniCounterMax, aniTimer, aniTimerMax, aniCount;   // animation variables
        public enums.EntityState entityState;
        public int speed, hp, hpMax, spriteScale;

        public Entities() {

        }

        public virtual void Update() {

        }
        public virtual SpriteBatch Draw(SpriteBatch spriteBatch){

            spriteBatch.Draw(this.spriteSheets[(int)this.entityState], this.drawBox, this.sourceRectangle[(int)this.entityState][this.aniCount], Color.White);

            return spriteBatch;
        }
        public virtual void Movement(int[] move)
        {
            if (move[0] != 0 && move[1] != 0)
            {   // here we handle movement in two Directions at once.
                this.position[0] += move[0] * (this.speed * 3 / 4);
                this.position[1] += move[1] * (this.speed * 3 / 4);

            }
            else 
            {   // here everything else.
                this.position[0] += move[0] * this.speed;
                this.position[1] += move[1] * this.speed;
            }
            this.drawBox.X = this.position[0];
            this.drawBox.Y = this.position[1];
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
            // so we know where to Draw the sprite:
            this.drawBox = new Rectangle(this.position[0], this.position[1], this.spriteSize * this.spriteScale, this.spriteSize * this.spriteScale);
        }

        public void Animation() {
        
        
        }
    }
}
