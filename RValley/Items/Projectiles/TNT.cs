using Microsoft.Xna.Framework.Graphics;
using RValley.Entities;
using RValley.Entities.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Items.Projectiles
{
    internal class TNT : Projectile
    {
        bool hit;
        public TNT(int damage, int[] targetPos, Texture2D[] sprite, int[] playerPos)
        {
            this.damage = damage;       // Damage is done on ani frame 1
            base.targetPos = targetPos;
            base.sprite = sprite[0];
            base.explosionSprites = sprite[1];
            base.createSourceRectangles();
            base.position = new int[2] { playerPos[0], playerPos[1] };
            base.aniCount = 0;
            base.aniTime = 100;
            base.exploding = false;
            this.hit = false;
            base.range = 50;
            base.speed = 5;
            base.getStaticMovement();
            base.rectangle = new Microsoft.Xna.Framework.Rectangle(base.position[0], base.position[1], base.sprite.Height, base.sprite.Height);

            base.animationTimer = new Stopwatch();
            base.animationTimer.Start();
        }

        public override bool Update(List<Player> enti)          
        {
            // here we check if the projectile is exploding or not. If it is not exploding we just update the projectile like normal,
            // but if it is exploding we check if the animation is done and if we need to change the hitbox of the explosion.
            if (!base.exploding)
            {
                base.Update();
            }
            else
            {
                // here we check if the explosion animation is done and if we need to change the hitbox of the explosion.
                if (base.rectangle.Width != base.explosionSprites.Height * 2)
                {
                    base.rectangle.X = base.rectangle.X - (base.rectangle.Width * 2);
                    base.rectangle.Y = base.rectangle.Y - (base.rectangle.Height * 2);

                    base.position[0] = base.rectangle.X;
                    base.position[1] = base.rectangle.Y;

                    base.rectangle.Width = base.explosionSprites.Height * 2;
                    base.rectangle.Height = base.explosionSprites.Height * 2;
                }
                // here we check if the animation is done and if we need to deal damage to the entities in range.
                for (int i = 0; i < enti.Count; i++) {
                    // is a player/entity in range of the explosion?
                    if (this.range <= Math.Abs(base.rectangle.Center.X - enti[i].hitBox.Center.X) + Math.Abs(base.rectangle.Center.Y - enti[i].hitBox.Center.Y) && !this.hit && base.aniCount > 0) {
                        enti[i].TakeDamage(this.damage);
                        base.exploding = true;
                        base.aniCount = 0;
                        base.aniCountMax = base.expSourceRectangles.Length - 1;
                        this.hit = true;
                    }
                }
            }
            return base.Animation();
        }
    }
}
