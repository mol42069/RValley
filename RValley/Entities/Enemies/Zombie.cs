﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RValley.Entities.Enemies
{
    internal class Zombie : Enemies             // For now these are goblins.
    {

        public Zombie(int[]  startingPos, int[] targetOffset, int aniCount) {

            base.targetOffset = targetOffset;

            base.damage = 5;

            base.speed = 5;
            base.hpMax = 100;
            base.hp = base.hpMax;
            base.reach = 100;
            base.distance = 1000000;
            base.position = startingPos;
            
            base.drawPosition = base.position;
            base.spriteScale = 1;
            base.hitBoxOffset = new int[2] { (int)(56 * base.spriteScale), (int)(100 * base.spriteScale) };

            base.hitBox = new Rectangle(base.position[0] + base.hitBoxOffset[0], base.position[1] + base.hitBoxOffset[1], base.spriteSize - base.hitBoxOffset[0] * 2, base.spriteSize - base.hitBoxOffset[1]);

            base.hitBox.Width = (int)(base.spriteSize * base.spriteScale - base.hitBoxOffset[0] * 2);
            base.hitBox.Height = (int)(base.spriteSize * base.spriteScale - base.hitBoxOffset[1]);

            
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

        }

    }
}
