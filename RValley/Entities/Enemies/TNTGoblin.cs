using Microsoft.Xna.Framework.Graphics;
using RValley.Items.Projectiles;
using RValley.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities.Enemies
{
    internal class TNTGoblin : Enemies
    {

        int attackFrequency, lAttack;
        public TNTGoblin(int[] startingPos, int[] targetOffset, int aniCount)
        {
            base.targetOffset = targetOffset;

            base.damage = 50;

            base.speed = 5;
            base.hpMax = 500;
            base.hp = base.hpMax;
            base.reach = 700;
            base.distance = 1000000;
            base.position = startingPos;

            base.drawPosition = base.position;
            base.spriteScale = 1.0f;
            base.projectiles = new List<Items.Projectiles.Projectile>();
            this.attackFrequency = 40;
            this.lAttack = 0;

            base.aniCount = 0;
            base.entityState = enums.EntityState.IDLE_L;

            // HERE WE SET ALL THE ANIMATION VARIABLES.
            base.animationTimer = new System.Diagnostics.Stopwatch();
            base.animationTimer.Start();
            base.aniCount = aniCount;
            base.aniCountMax = 0;
            base.aniTimerMax = new long[(int)enums.EntityState.MAX] { 50, 50, 100 }; // 0 = RUN | 1 = IDLE | 2 = PATTACK | (see enums.EntityState)
            base.lastMovement = new float[2] { 0, 0 };
            base.direction = false;

            base.hitBoxOffset = new int[2] { 60, 112 };

        }

        public override void Update(List<Player> player, MapManager mapManager)
        {
            // Update the Projectiles if there are any
            if (base.projectiles.Count != 0)
            {
                for (int i = 0; i < base.projectiles.Count; i++)
                {
                    if (base.projectiles[i].Update(player)) {

                        base.projectiles.RemoveAt(i);
                        base.alreadyAttacked = false;
                    }
                }
            }

            if (mapManager.backgroundSprite != null)
            {
                base.hitBox.X = base.position[0] + base.hitBoxOffset[0];
                base.hitBox.Y = base.position[1] + base.hitBoxOffset[1];

                if (this.target != null)
                {
                    int distx = ((this.target.hitBox.Center.X + this.targetOffset[0]) - base.hitBox.Center.X);
                    if (distx < 0) distx *= -1;

                    int disty = ((this.target.hitBox.Center.Y + this.targetOffset[1]) - base.hitBox.Center.Y);
                    if (disty < 0) disty *= -1;

                    this.distance = distx + disty;
                }

                if (this.distance <= base.reach)
                {
                    if (!base.alreadyAttacked)
                    {
                        // here we will do the attacks.
                        base.lastMovement[0] = 0;
                        base.lastMovement[1] = 0;
                        base.drawPosition = mapManager.calculateDrawPositionEntity(this.position);
                        base.drawBox.X = base.drawPosition[0];
                        base.drawBox.Y = base.drawPosition[1];
                        this.PrimaryAttack(player[0]);
                    }
                }
                else
                {
                    base.Movement(base.AI(player), mapManager);

                    base.drawPosition = mapManager.calculateDrawPositionEntity(base.position);

                    base.drawBox.X = base.drawPosition[0];
                    base.drawBox.Y = base.drawPosition[1];
                }
                base.Update(mapManager);
                if (this.lAttack > 0)
                {
                    this.lAttack--;
                }
                else 
                { 
                    base.alreadyAttacked = false;
                }

            }
        }/*

        public override float[] AI(List<Player> player) {
            float[] nextMove = new float[2];
            nextMove[0] = 0;   
            nextMove[1] = 0;   

            nextMove = base.AI(player);

            for (int i = 0; i < player.Count; i++) {
                // distance to player is smaller then the reach of the entity so that we dont run into the player but stop
                if (Math.Abs(player[i].hitBox.Center.X - (base.position[0] + (nextMove[0] * base.speed))) + Math.Abs(player[i].hitBox.Center.Y - (base.position[1] + (nextMove[1] * base.speed))) < base.reach) { 
                    nextMove[0] = 0;
                    nextMove[1] = 0;
                }
            }
            return nextMove;
        }*/

        public override void PrimaryAttack(Player player)
        {
            // we use this for single target attacks.

            // first we animate the attack windup and when we are far enough we deal damage.
            base.primaryAttackActive = true;
            if (player.position[0] <= base.position[0])
            {
                base.direction = true;
            }
            else
            {
                base.direction = false;
            }

            base.alreadyAttacked = true;
            this.lAttack = this.attackFrequency;

            int[] tPos = new int[2];
            int[] targetPos = new int[2];
            tPos[0] = player.hitBox.Center.X;
            tPos[1] = player.hitBox.Center.Y;
            Array.Copy(tPos, targetPos, 2);

            base.projectiles.Add(new TNT(10, targetPos, base.projectileSprites[(int)ProjEnums.Projectile.TNT], base.position));            
       }
    }
}
