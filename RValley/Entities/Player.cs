using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities
{
    internal class Player : Entities
    {

        public Player() 
        {
            base.speed = 20;
            base.hpMax = 100;
            base.hp = base.hpMax;
            base.position = new int[2] {200, 200};
            base.hitBox = new Rectangle(base.position[0] + 10, base.position[1] + 10, base.spriteSize - 20, base.spriteSize - 20);
            base.spriteScale = 2;
            base.aniCount = 0;
            base.entityState = enums.EntityState.IDLE_L;

            // HERE WE SET ALL THE ANIMATION VARIABLES.
            base.animationTimer = new System.Diagnostics.Stopwatch();
            base.animationTimer.Start();
            base.aniCount = 0;
            base.aniCountMax = 0;
            base.aniTimerMax = new long[(int)enums.EntityState.MAX] {25, 200, 100}; // 0 = RUN | 1 = IDLE | 2 = DEATH | (see enums.EntityState)
            base.lastMovement = new int[2] {0, 0};
            this.direction = false;

        }

        public override void Update()
        {

            base.Update();
        }




    }
}
