using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities
{

// WE WANT TO DO EVERYTHING (WHAT WE DO IN CHILDREN) WE CAN IN THIS CLASS:
    internal class Entities
    {
        public int[] position, lastMovement , drawPosition;
        protected int spriteSize;
        protected Rectangle hitBox;   // if we decide to add headshots or other stuff we might want to add this here... or save those somewhere else.
        protected Rectangle drawBox;
        public Texture2D[] spriteSheets;
        protected Rectangle[][] sourceRectangle;
        protected int  aniCountMax, aniCount;   // animation variables
        protected long[] aniTimerMax;
        public enums.EntityState entityState;
        public int speed, hp, hpMax, spriteScale, range;
        protected Stopwatch animationTimer;
        protected bool direction, spriteRotation;               // true = left | false = right

        public Entities() {

        }

        public virtual void Update() {

            if (this.lastMovement[0] < 0)
            {
                // this.aniCount = 0;
                this.direction = true;
                switch (this.entityState)
                {
                    case enums.EntityState.IDLE_R:
                        this.entityState = enums.EntityState.IDLE_L;
                        break;

                    case enums.EntityState.RUN_R:
                        this.entityState = enums.EntityState.RUN_L;
                        break;

                    case enums.EntityState.DEATH_R:
                        this.entityState = enums.EntityState.DEATH_L;
                        break;
                }
            }
            else if (this.lastMovement[0] > 0)
            {
                // this.aniCount = 0;
                this.direction = false;
                switch (this.entityState)
                {
                    case enums.EntityState.IDLE_L:
                        this.entityState = enums.EntityState.IDLE_R;
                        break;

                    case enums.EntityState.RUN_L:
                        this.entityState = enums.EntityState.RUN_R;
                        break;

                    case enums.EntityState.DEATH_L:
                        this.entityState = enums.EntityState.DEATH_R;
                        break;
                }
            }

            // here we update the entity state as well as switch the aniCount Max so we dont go IndexOutOfBounds.

            if (this.lastMovement[0] == 0 && this.lastMovement[1] == 0 &&  !(this.entityState == enums.EntityState.IDLE_R || this.entityState == enums.EntityState.IDLE_L))
            {
                if (this.direction)
                {

                    this.entityState = enums.EntityState.IDLE_L;
                    this.aniCountMax = this.sourceRectangle[(int)this.entityState].Length;
                    this.aniCount = this.aniCountMax - 1;
                }
                else
                {

                    this.entityState = enums.EntityState.IDLE_R;
                    this.aniCountMax = this.sourceRectangle[(int)this.entityState].Length;
                    this.aniCount = 0;
                }
            }
            else if(this.hp <= 0 && !(this.entityState == enums.EntityState.DEATH_R || this.entityState == enums.EntityState.DEATH_L))
            {
                if (this.direction)
                {

                    this.entityState = enums.EntityState.DEATH_L;
                    this.aniCountMax = this.sourceRectangle[(int)this.entityState].Length;
                    this.aniCount = this.aniCountMax - 1;
                }
                else
                {

                    this.entityState = enums.EntityState.DEATH_R;
                    this.aniCountMax = this.sourceRectangle[(int)this.entityState].Length;
                    this.aniCount = 0;
                }
            }
            else if (!(this.entityState == enums.EntityState.RUN_R || this.entityState == enums.EntityState.RUN_L) && (this.lastMovement[0] != 0 || this.lastMovement[1] != 0))
            {
                if (this.direction)
                {

                    this.entityState = enums.EntityState.RUN_L;
                    this.aniCountMax = this.sourceRectangle[(int)this.entityState].Length;
                    this.aniCount = this.aniCountMax - 1;
                }
                else
                {

                    this.entityState = enums.EntityState.RUN_R;
                    this.aniCountMax = this.sourceRectangle[(int)this.entityState].Length;
                    this.aniCount = 0;
                }
            }
          
        }
        public virtual SpriteBatch Draw(SpriteBatch spriteBatch){

            spriteBatch.Draw(this.spriteSheets[(int)this.entityState], this.drawBox, this.sourceRectangle[(int)this.entityState][this.aniCount], Color.White);
            
            return spriteBatch;
        }


        public virtual void Movement(int[] move, MapManager mapManager)
        {
            this.lastMovement = move;
            if (move[0] != 0 && move[1] != 0)
            {   // here we handle movement in two Directions at once.
                this.position[0] += move[0] * (this.speed * 4/5);
                this.position[1] += move[1] * (this.speed * 4/5);

            }
            else 
            {   // here everything else.
                this.position[0] += move[0] * this.speed;
                this.position[1] += move[1] * this.speed;
            }

            // here we check that the entity wont move out of bounds_
            // X-Axis:
            if (this.position[0] < 0)
            {
                this.position[0] = 0;
            }
            else if (this.position[0] > mapManager.backgroundSprite.Width - this.spriteSize * 2)
            {
                this.position[0] = mapManager.backgroundSprite.Width - this.spriteSize * 2;
            }
            // Y-Axis:
            if (this.position[1] < 0)
            {
                this.position[1] = 0;
            }
            else if (this.position[1] > mapManager.backgroundSprite.Height - this.spriteSize * 2)
            {
                this.position[1] = mapManager.backgroundSprite.Height - this.spriteSize * 2;
            }


            this.drawBox.X = this.drawPosition[0];
            this.drawBox.Y = this.drawPosition[1];
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
            if (this.animationTimer.ElapsedMilliseconds >= this.aniTimerMax[(int)decimal.Round(((int)this.entityState / 2), 0)]) {
                this.animationTimer.Stop();
                this.animationTimer.Reset();
                this.animationTimer.Start();
                
                if (this.direction)
                {
                    this.aniCount--;

                    if (this.aniCount < 0) this.aniCount = this.aniCountMax - 1;
                }
                else 
                {
                    this.aniCount++;
                    if (this.aniCount >= this.aniCountMax) this.aniCount = 0;
                }
            }        
        }
    }
}
