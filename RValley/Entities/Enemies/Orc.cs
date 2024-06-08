using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities.Enemies
{
    internal class Orc : Enemies
    {
        public Orc(int[] startingPos,int[] targetOffset, int aniCount)
        {
            base.targetOffset = targetOffset;

            base.damage = 50;

            base.speed = 5;
            base.hpMax = 500;
            base.hp = base.hpMax;
            base.reach = 200;
            base.distance = 1000000;
            base.position = startingPos;

            base.drawPosition = base.position;
            base.spriteScale = 1.0f;



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
    }
}
