using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using RValley.Entities.Enemies;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace RValley.Items.Projectiles
{
    public class Projectile
    {
        public int damage, range, speed, aniCount, aniCountMax, aniTime;
        protected Stopwatch stopwatch;
        public int[] targetPos, position;
        protected float[] staticMovement;
        protected Texture2D sprite, explosionSprites;
        protected Rectangle[] rectangles, expSourceRectangles;
        public Rectangle rectangle, drawRectangle;
        protected bool exploding;                                               // also used for making the projectile stop moveing
        protected Stopwatch animationTimer;

        public Projectile()
        {

        }

        public virtual bool Update(List<Enemies> enemies)
        {

            int distx = this.position[0] - this.targetPos[0];
            if (distx < 0)
            {
                distx *= -1;
            }

            int disty = this.position[1] - this.targetPos[1];
            if (disty < 0)
            {
                disty *= -1;
            }

            int distance = distx + disty;

            if (distance <= this.range)
            {
                this.exploding = true;
            }

            return false;
        }

        public virtual void Update() {
            int distx = this.position[0] - this.targetPos[0];
            if (distx < 0)
            {
                distx *= -1;
            }

            int disty = this.position[1] - this.targetPos[1];
            if (disty < 0)
            {
                disty *= -1;
            }

            int distance = distx + disty;

            if (distance <= this.range)
            {
                this.exploding = true;
            }

            this.getStaticMovement();

            this.position[0] += (int)(this.staticMovement[0] * (float)this.speed);
            this.position[1] += (int)(this.staticMovement[1] * (float)this.speed);
            this.rectangle.X = this.position[0];
            this.rectangle.Y = this.position[1];

            return;



        }
        protected void getStaticMovement()
        {

            int distx = this.rectangle.Center.X - this.targetPos[0];
            if (distx < 0)
            {
                distx *= -1;
            }

            int disty = this.rectangle.Center.Y - this.targetPos[1];
            if (disty < 0)
            {
                disty *= -1;
            }

            int distance = distx + disty;

            distx = this.targetPos[0] - this.rectangle.Center.X;
            disty = this.targetPos[1] - this.rectangle.Center.Y;

            this.staticMovement = new float[2]
            {
                (float) distx/ (float)distance,
                (float) disty/ (float)distance
            };

        }
        protected bool Animation()
        {

            if (this.animationTimer.ElapsedMilliseconds >= this.aniTime)
            {

                this.animationTimer.Stop();
                this.animationTimer.Reset();
                this.animationTimer.Start();

                

                if (this.aniCount + 1 > this.aniCountMax)
                {
                    this.aniCount = 0;
                }
                else
                {
                    this.aniCount++;
                }

                if (this.aniCount == 1 && this.exploding)
                {
                    return true;
                }
                else if (this.exploding)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public SpriteBatch Draw(SpriteBatch spriteBatch, MapManager mapManager)
        {   
            int[] tempPos = new int[2] {this.rectangle.X, this.rectangle.Y};
            int[] drawPos = mapManager.calculateDrawPositionEntity(tempPos);

            this.drawRectangle = new Rectangle(drawPos[0], drawPos[1], this.rectangle.Width, this.rectangle.Height);

            if (this.explosionSprites != null)
            {       // HERE WE DRAW THE EXPLODING PROJECTILES

                if (!this.exploding)
                {
                    spriteBatch.Draw(this.sprite, this.drawRectangle, this.rectangles[this.aniCount], Color.White);
                }
                else
                {
                    int[] scaledPos = { this.rectangle.X, this.rectangle.Y };
                    int[] drawPoss = mapManager.calculateDrawPositionEntity(scaledPos);

                    this.drawRectangle.X = drawPoss[0];
                    this.drawRectangle.Y = drawPoss[1];

                    spriteBatch.Draw(this.explosionSprites, this.drawRectangle, this.expSourceRectangles[this.aniCount], Color.White);

                }

            }
            else
            {       // HER WE DRAW THE NON EXPLOSIVE PROJECTILES

            }

            return spriteBatch;
        }
        protected void createSourceRectangles()
        {
            this.rectangles = new Rectangle[(int)((float)this.sprite.Width / (float)this.sprite.Height)];

            for (int i = 0; i < this.rectangles.Length; i++)
            {
                this.rectangles[i] = new Rectangle(i * this.sprite.Height, 0, this.sprite.Height, this.sprite.Height);
            }
            this.aniCountMax = this.rectangles.Length - 1;

            this.expSourceRectangles = new Rectangle[(int)((float)this.explosionSprites.Width / (float)this.explosionSprites.Height)];

            for (int i = 0; i < this.expSourceRectangles.Length; i++)
            {
                this.expSourceRectangles[i] = new Rectangle(i * this.explosionSprites.Height, 0, this.explosionSprites.Height, this.explosionSprites.Height);
            }
        }
    }
}
