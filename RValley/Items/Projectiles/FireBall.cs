using Microsoft.Xna.Framework.Graphics;
using RValley.Entities.Enemies;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Items.Projectiles
{
    public class FireBall : Projectile
    {
        public FireBall(int damage, int[] targetPos, Texture2D[] sprite, int[] playerPos)
        {
            this.damage = damage;
            this.targetPos = targetPos;
            base.sprite = sprite[1];
            base.explosionSprites = sprite[0];
            base.createSourceRectangles();
            base.position = new int[2] {playerPos[0], playerPos[1]};
            base.stopwatch = new System.Diagnostics.Stopwatch();
            base.aniCount = 0;
            base.aniTime = 50;
            base.exploding = false;
            base.range = 500;
            base.speed = 20;
            base.getStaticMovement();
            base.rectangle = new Microsoft.Xna.Framework.Rectangle(base.position[0], base.position[1], base.sprite.Height, base.sprite.Height);
        }

        public override bool Update(List<Enemies> enemies)          // return true if the projectile is to be deleted
        {
            if (!base.exploding)
            {
                base.position[0] += (int)(base.staticMovement[0] * base.speed);
                base.position[1] += (int)(base.staticMovement[1] * base.speed);

                int distx = base.position[0] - base.targetPos[0];
                if (distx < 0)
                {
                    distx *= -1;
                }

                int disty = base.position[1] - base.targetPos[1];
                if (disty < 0)
                {
                    disty *= -1;
                }
                int distance = distx + disty;

                if (distance <= base.range)
                {
                    base.exploding = true;
                    base.aniCount = 0;
                    base.aniCountMax = base.expSourceRectangles.Length - 1; ;
                }
            }
            else 
            {
                base.rectangle.Width = base.explosionSprites.Height;
                base.rectangle.Height = base.explosionSprites.Height;            
            }


            return base.Animation(); ;
        }

    }
}
