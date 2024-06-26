﻿using Microsoft.Xna.Framework;
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
    public class Entities
    {
        public int[] position, drawPosition;
        public float []lastMovement;
        public float spriteScale;
        protected int spriteSize;
        public Rectangle hitBox;   // if we decide to add headshots or other stuff we might want to add this here... or save those somewhere else.
        public Rectangle drawBox;
        public Texture2D[] spriteSheets;
        public Texture2D[][] projectileSprites;
        protected Rectangle[][] sourceRectangle;
        public int  aniCountMax, aniCount;   // animation variables
        protected long[] aniTimerMax;
        public enums.EntityState entityState;
        public int speed, hp, hpMax, reach;
        protected Stopwatch animationTimer;
        public bool direction, spriteRotation;               // true = left | false = right
        public int[] hitBoxOffset;                          // hitBoxOffset is to size and place the hitboxes correctly.
        public bool primaryAttackActive, primaryAttackFinished;
        public int primaryAttackAnimationCount, primaryAttackAnimationCountMax;

        public Entities() {

        }

        public virtual void Update(MapManager mapManager) 
        {
            if (this.lastMovement != null)
            {
                // HERE WE ENFORCE THE MAPLIMIT
                if (this.hitBox.X < 0)
                {
                    this.hitBox.X = 0;
                }
                else if (this.hitBox.X > mapManager.backgroundSprite.Width - this.hitBox.Width)
                {
                    this.hitBox.X = mapManager.backgroundSprite.Width - this.hitBox.Width;
                }
                // Y-Axis:
                if (this.hitBox.Y < 0)
                {
                    this.hitBox.Y = 0;
                }
                else if (this.hitBox.Y > mapManager.backgroundSprite.Height - this.hitBox.Height)
                {
                    this.hitBox.Y = mapManager.backgroundSprite.Height - this.hitBox.Height;
                }

                // HERE WE DO THE ATTACKS

                if (this.primaryAttackActive) 
                {

                    if (this.direction)
                    {
                        if (this.entityState != enums.EntityState.PATTACK_L)
                        {
                            this.entityState = enums.EntityState.PATTACK_L;
                            this.aniCountMax = this.sourceRectangle[(int)enums.EntityState.PATTACK_L].Length;
                            this.aniCount = this.aniCountMax - 1;
                        }
                    }
                    else
                    {
                        if (this.entityState != enums.EntityState.PATTACK_R)
                        {
                            this.entityState = enums.EntityState.PATTACK_R;
                            this.aniCountMax = this.sourceRectangle[(int)enums.EntityState.PATTACK_L].Length;
                            this.aniCount = 0;
                        }
                    }
                    return;
                }



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
                    }
                }

                // here we update the entity state as well as switch the aniCount Max so we dont go IndexOutOfBounds.

                if (this.lastMovement[0] == 0 && this.lastMovement[1] == 0 && !(this.entityState == enums.EntityState.IDLE_R || this.entityState == enums.EntityState.IDLE_L))
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

        }
        public virtual SpriteBatch Draw(SpriteBatch spriteBatch,MapManager mapManager){
            try
            {
                spriteBatch.Draw(this.spriteSheets[(int)this.entityState], this.drawBox, this.sourceRectangle[(int)this.entityState][this.aniCount], Color.White);
                
                /*
                Texture2D _texture;
                _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });
                spriteBatch.Draw(_texture, this.hitBox, Color.White);
                */

            }
            catch 
            {
            
            }
            return spriteBatch;
        }


        public virtual void Movement(float[] move, MapManager mapManager)
        {
            if(move[0] != 0 && move[0] != 0) this.primaryAttackActive = false;

            if (this.drawPosition == null) return;

            this.lastMovement = move;

            if (move[0] != 0 && move[1] != 0)
            {   // here we handle movement in two Directions at once.
                this.position[0] += (int)(move[0] * (this.speed * 4/5));
                this.position[1] += (int)(move[1] * (this.speed * 4/5));

            }
            else 
            {   // here everything else.
                this.position[0] += (int)(move[0] * this.speed);
                this.position[1] += (int)(move[1] * this.speed);
            }

            this.hitBox.X = this.position[0] + this.hitBoxOffset[0];
            this.hitBox.Y = this.position[1] + this.hitBoxOffset[1];

            // here we check that the entity wont move out of bounds_
            // X-Axis:
            if (this.hitBox.X < 0)
            {
                this.hitBox.X = 0;
            }
            else if (this.hitBox.X > mapManager.backgroundSprite.Width - this.hitBox.Width)
            {
                this.hitBox.X = mapManager.backgroundSprite.Width - this.hitBox.Width;
            }
            // Y-Axis:
            if (this.hitBox.Y < 0)
            {
                this.hitBox.Y = 0;
            }
            else if (this.hitBox.Y > mapManager.backgroundSprite.Height - this.hitBox.Height)
            {
                this.hitBox.Y = mapManager.backgroundSprite.Height - this.hitBox.Height;
            }
            this.position[0] = this.hitBox.X - this.hitBoxOffset[0];
            this.position[1] = this.hitBox.Y - this.hitBoxOffset[1];


        }

        // we only want spriteSheets for one playerClass.
        public void LoadContent(Texture2D[] spriteSheets, Rectangle[][] sourceRectangle) { 
            this.spriteSheets = spriteSheets;
            this.sourceRectangle = sourceRectangle;
            this.spriteSize = (int)(this.spriteSheets[0].Height * this.spriteScale);

            this.hitBoxOffset = new int[2] { (int)(56 * this.spriteScale), (int)((this.spriteSize / 2) * this.spriteScale) };
            
            this.hitBox = new Rectangle(this.position[0] + this.hitBoxOffset[0], this.position[1] + this.hitBoxOffset[1], this.spriteSize - this.hitBoxOffset[0] * 2, this.spriteSize - this.hitBoxOffset[1]);

           
            this.hitBox.Width = (int)((this.spriteSize - this.hitBoxOffset[0] * 2) * this.spriteScale);
            this.hitBox.Height = (int)(this.spriteSize * this.spriteScale - this.hitBoxOffset[1]);


            this.drawBox = new Rectangle(this.position[0], this.position[1], (int)(this.spriteSize * this.spriteScale), (int)(this.spriteSize * this.spriteScale));

        }

        public void LoadContent(Texture2D[] spriteSheets) {
            this.spriteSheets = spriteSheets;
            this.CreatesourceRectangles();
        }

        public void LoadProjectileContent(Texture2D[][] spriteSheets)
        {
            this.projectileSprites = spriteSheets;
        }


        private void CreatesourceRectangles() 
        {
            this.sourceRectangle = new Rectangle[this.spriteSheets.Length][];
            for (int i = 0; i < this.spriteSheets.Length; i++) 
            {
                this.spriteSize = this.spriteSheets[i].Height;
                this.sourceRectangle[i] = new Rectangle[(this.spriteSheets[i].Width / this.spriteSize)];

                for (int j = 0; j < this.sourceRectangle[i].Length; j++) 
                {
                    this.sourceRectangle[i][j] = new Rectangle(j * this.spriteSize, 0, spriteSize, spriteSize);                
                }
            }
            // so we know where to Draw the sprite:
            this.drawBox = new Rectangle(this.position[0], this.position[1], (int)(this.spriteSize * this.spriteScale), (int)(this.spriteSize * this.spriteScale));
        }

        public void Animation() {
            if (this.animationTimer.ElapsedMilliseconds >= this.aniTimerMax[(int)decimal.Round(((int)this.entityState / 2), 0)]) {
                
                this.animationTimer.Stop();
                this.animationTimer.Reset();
                this.animationTimer.Start();
                
                if (this.direction)
                {
                    this.aniCount--;

                    if (this.aniCount <= 0) {
                        this.aniCount = this.aniCountMax - 1;
                        if (this.primaryAttackActive)
                        {
                            this.primaryAttackFinished = true;
                            this.primaryAttackActive = false;
                        }
                    }
                }
                else 
                {
                    this.aniCount++;
                    if (this.aniCount >= this.aniCountMax)
                    {
                        this.aniCount = 0;
                        if (this.primaryAttackActive)
                        {
                            this.primaryAttackFinished = true;
                            this.primaryAttackActive = false;
                        }
                    }
                }
                
            }        
        }

        public void TakeDamage(int damage) {
            this.hp -= damage;        
        }

        private void PrimaryAttackAnimation(Texture2D attackSprite) {


        }

    }
}
