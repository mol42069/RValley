using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities.Enemies
{
    internal class Zombie : Enemies
    {

        public Zombie(int[]  startingPos, int[] targetOffset, int aniCount) {

            base.targetOffset = targetOffset;

            base.speed = 10;
            base.hpMax = 100;
            base.hp = base.hpMax;
            base.reach = 100;
            base.distance = 1000000;
            base.position = startingPos;
            
            base.drawPosition = base.position;
            base.hitBox = new Rectangle(base.position[0] + 10, base.position[1] + 10, base.spriteSize - 20, base.spriteSize - 20);

            this.hitBox.Width = this.spriteSize - 92;
            this.hitBox.Height = this.spriteSize - 92;

            base.spriteScale = 1;
            base.aniCount = 0;
            base.entityState = enums.EntityState.IDLE_L;

            // HERE WE SET ALL THE ANIMATION VARIABLES.
            base.animationTimer = new System.Diagnostics.Stopwatch();
            base.animationTimer.Start();
            base.aniCount = aniCount;
            base.aniCountMax = 0;
            base.aniTimerMax = new long[(int)enums.EntityState.MAX] { 50, 50, 100 }; // 0 = RUN | 1 = IDLE | 2 = DEATH | (see enums.EntityState)
            base.lastMovement = new float[2] { 0, 0 };
            base.direction = false;

        }

    }
}
